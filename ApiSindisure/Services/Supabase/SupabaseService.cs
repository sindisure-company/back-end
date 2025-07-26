using Microsoft.AspNetCore.Mvc.Routing;
using Supabase;
using Supabase.Gotrue;
using SupabaseClient = Supabase.Client;


namespace ApiSindisure.Services.Supabase
{
    public class SupabaseService
    {
        private readonly SupabaseClient _client;
        private readonly string _url;
        private readonly string _anonKey;

        public SupabaseService(IConfiguration configuration)
        {
            _url = configuration["Supabase:Url"];
            _anonKey = configuration["Supabase:AnonKey"];

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
                return null;
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
    }
}
