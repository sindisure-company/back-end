using ApiSindisure.Domain.Interfaces.Apps.Login;
using ApiSindisure.Domain.ViewModel.Login;
using ApiSindisure.Domain.ViewModel.UserRegisterViewModel;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.Login
{
    public class LoginApp : ILoginApp
    {
        private readonly SupabaseService _supabaseService;
        private readonly ILogger<LoginApp> _logger;
        public string LogId { get; set; }

        public LoginApp(SupabaseService supabaseService, ILogger<LoginApp> logger)
        {
            _supabaseService = supabaseService;
            _logger = logger;
            LogId = Guid.NewGuid().ToString();

        }

        public async Task<LoginViewModel.Response> HandleAsync(LoginViewModel.Request request, CancellationToken cancellationToken)
        {
            try
            {
                var session = await _supabaseService.SignIn(request.Email, request.Password);

                var user = await _supabaseService.GetUser(session.AccessToken);

                // Mapeando dados do Supabase -> LoginViewModel.Response
                var response = new LoginViewModel.Response
                {
                    AccessToken = session.AccessToken,
                    TokenType = session.TokenType,
                    ExpiresIn = session.ExpiresIn,
                    ExpiresAt = new DateTimeOffset(session.ExpiresAt()).ToUnixTimeSeconds(),
                    RefreshToken = session.RefreshToken,
                    User = new LoginViewModel.UserResponse
                    {
                        Id = user.Id,
                        Aud = user.Aud,
                        Role = user.Role,
                        Email = user.Email,
                        EmailConfirmedAt = user.EmailConfirmedAt,
                        Phone = user.Phone,
                        ConfirmedAt = user.ConfirmedAt,
                        LastSignInAt = user.LastSignInAt,
                        AppMetadata = new LoginViewModel.AppMetadata
                        {
                            Provider = user.AppMetadata?["provider"]?.ToString(),
                            Providers = user.AppMetadata?["providers"] as List<string>
                                ?? new List<string>()
                        },
                        UserMetadata = new LoginViewModel.UserMetadata
                        {
                            Address = user.UserMetadata?["address"]?.ToString(),                            
                            City = user.UserMetadata?["city"]?.ToString(),
                            DateOfBirth = Convert.ToDateTime(user.UserMetadata?["date_of_birth"]?.ToString()),
                            DocumentNumber = user.UserMetadata?["document_number"]?.ToString(),
                            Email = user.UserMetadata?["email"]?.ToString(),
                            EmailVerified = user.UserMetadata?["email_verified"] != null &&
                                            bool.TryParse(user.UserMetadata["email_verified"].ToString(), out var ev) ? ev : false,
                            FirstName = user.UserMetadata?["first_name"]?.ToString(),
                            LastName = user.UserMetadata?["last_name"]?.ToString(),                           
                            Number = user.UserMetadata?["number"]?.ToString(),
                            Phone = user.UserMetadata?["phone"]?.ToString(),
                            PhoneVerified = user.UserMetadata?["phone_verified"] != null &&
                                            bool.TryParse(user.UserMetadata["phone_verified"].ToString(), out var pv) ? pv : false,
                            State = user.UserMetadata?["state"]?.ToString(),
                            Sub = user.UserMetadata?["sub"]?.ToString(),

                        },
                        Identities = user.Identities?.Select(i => new LoginViewModel.Identity
                        {
                            IdentityId = i.IdentityId,
                            Id = i.Id,
                            UserId = i.UserId,
                            IdentityData = new LoginViewModel.UserMetadata
                            {
                                Address = i.IdentityData?["address"]?.ToString(),                                
                                City = i.IdentityData?["city"]?.ToString(),
                                DateOfBirth = Convert.ToDateTime(i.IdentityData?["date_of_birth"]?.ToString()),
                                DocumentNumber = i.IdentityData?["document_number"]?.ToString(),
                                Email = i.IdentityData?["email"]?.ToString(),
                                EmailVerified = i.IdentityData?["email_verified"] != null &&
                                                bool.TryParse(i.IdentityData["email_verified"].ToString(), out var iev) ? iev : false,
                                FirstName = i.IdentityData?["first_name"]?.ToString(),
                                LastName = i.IdentityData?["last_name"]?.ToString(),                              
                                Number = i.IdentityData?["number"]?.ToString(),
                                Phone = i.IdentityData?["phone"]?.ToString(),
                                PhoneVerified = i.IdentityData?["phone_verified"] != null &&
                                                bool.TryParse(i.IdentityData["phone_verified"].ToString(), out var ipv) ? ipv : false,
                                State = i.IdentityData?["state"]?.ToString(),
                                Sub = i.IdentityData?["sub"]?.ToString(),

                            },
                            Provider = i.Provider,
                            LastSignInAt = i.LastSignInAt,
                            CreatedAt = i.CreatedAt,
                            UpdatedAt = i.UpdatedAt,
                            Email = i.IdentityData?["email"]?.ToString(),
                        }).ToList(),
                        CreatedAt = user.CreatedAt,
                        UpdatedAt = user.UpdatedAt,
                        IsAnonymous = user.IsAnonymous
                    }
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(LoginApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                return new LoginViewModel.Response
                {
                    ErrorMessage = "Erro interno do servidor"
                };
            }
        }

        public async Task<UserRegisterViewModel.Response> SignUp(UserRegisterViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var resultDictionary = ToDictionary(request);

                if (resultDictionary is null)
                    throw new ArgumentException("Erro ao converter para dictionary");                

                var response = await _supabaseService.SignUp(request, resultDictionary);

                if (response is null)
                    throw new ArgumentException("Erro ao retornar response");  

                return new UserRegisterViewModel.Response
                {
                    Id = response.Id,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(LoginApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception(ex.Message);
            }            
        }

        public async Task<LoginViewModel.Response> SendLinkResetPasswordAsync(LoginViewModel.ResetPassword request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _supabaseService.SendLinkResetPasswordForEmailAsync(request, cancellationToken);

                return new LoginViewModel.Response { };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(LoginApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Ocorreu um erro ao enviar link do reset de senha: " + ex.Message);
            }
        }  

        public async Task<LoginViewModel.Response> UpdateRecoverPasswordAsync(LoginViewModel.ResetPassword request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _supabaseService.UpdateRecoverPasswordAsync(request, cancellationToken);

                return new LoginViewModel.Response { };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(LoginApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Ocorreu um erro ao resetar a senha: " + ex.Message);
            }
        }      

        private Dictionary<string, object> ToDictionary(UserRegisterViewModel.CreateRequest request)
        {
            return new Dictionary<string, object>
            {
                { "date_of_birth", request.DateOfBirth ?? string.Empty },
                { "document_number", request.DocumentNumber ?? string.Empty },
                { "first_name", request.FirstName ?? string.Empty },
                { "last_name", request.LastName ?? string.Empty },
                { "address", request.Address ?? string.Empty },
                { "neighborhood", request.Neighborhood ?? string.Empty },
                { "number", request.Number ?? string.Empty },
                { "cep", request.Cep ?? string.Empty },
                { "complement", request.Complement ?? string.Empty },
                { "phone", request.Phone ?? string.Empty },
                { "city", request.City ?? string.Empty },
                { "state", request.State ?? string.Empty }
            };
        }
        
    }
}
