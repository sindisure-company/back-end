using ApiSindisure.Domain.ViewModel.UserRegisterViewModel;
using Supabase;
using Supabase.Gotrue;
using SupabaseClient = Supabase.Client;
using Newtonsoft.Json;
using System.Text;
using ApiSindisure.Domain.ViewModel.Login;
using System.Net.Http.Headers;


namespace ApiSindisure.Services.Supabase
{
    public class SupabaseService
    {
        private readonly SupabaseClient _client;
        private readonly string _url;
        private readonly string _anonKey;
        private readonly HttpClient _httpClient;

        public SupabaseService(IConfiguration configuration, HttpClient httpClient)
        {
            _url = configuration["Supabase:Url"];
            _anonKey = configuration["Supabase:ServiceRoles"];
            _httpClient = httpClient;

            var options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            if (_url is not null)
            {
                _client = new SupabaseClient(_url, _anonKey, options);
                _client.InitializeAsync().Wait();
            }
        }

        public async Task<Session?> SignIn(string email, string password)
        {
            try
            {
                var session = await _client.Auth.SignIn(email, password);
                return session;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User?> GetUser(string jwt)
        {
            return await _client.Auth.GetUser(jwt);
        }

        public async Task SignOut()
        {
            await _client.Auth.SignOut();
        }

        public SupabaseClient GetClient()
        {
            try
            {
                return _client;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter cliente Supabase: " + ex.Message);
            }

        }

        public async Task<User?> SignUp(UserRegisterViewModel.CreateRequest request, Dictionary<string, object>? userMetadata = null)
        {
            try
            {
                var options = new SignUpOptions
                {
                    Data = userMetadata
                };

                var session = await _client.Auth.SignUp(request.Login.Email, request.Login.Password, options);

                return session.User;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar usuário no Supabase: " + ex.Message, ex);
            }
        }

        public async Task<bool> SendLinkResetPasswordForEmailAsync(LoginViewModel.ResetPassword request, CancellationToken cancellationToken)
        {
            try
            {
                var requestBody = new
                {
                    email = request.Email,
                    redirect_to = request.RedirectTo
                };

                var requestApi = new HttpRequestMessage(HttpMethod.Post, $"{_url}/auth/v1/recover");
                requestApi.Headers.Add("apikey", _anonKey);
                requestApi.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestApi, cancellationToken);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao resetar senha no Supabase: " + ex.Message, ex);
            }

        }

        public async Task<bool> UpdateRecoverPasswordAsync(LoginViewModel.ResetPassword request, CancellationToken cancellationToken)
        {

           try
            {

                var response = await _client.Auth.Update(new UserAttributes { Password = request.NewPassword });

                if (response?.Email != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
             
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao resetar senha");
            }
        }

        public async Task<bool> UpdateUserAccessPassword(LoginViewModel.ResetPassword request, CancellationToken cancellationToken)
        {
            var requestUrl = $"{_url}/auth/v1/user";

            var body = new
            {
                password = request.NewPassword
            };

            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken);
            _httpClient.DefaultRequestHeaders.Add("apikey", _anonKey);

            var response = await _httpClient.PutAsync(requestUrl, content);

            return response.IsSuccessStatusCode;
        }
        

    }
}
