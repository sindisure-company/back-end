using ApiSindisure.Domain.Contants.Login;
using ApiSindisure.Domain.Dtos;
using ApiSindisure.Domain.Helpers;
using ApiSindisure.Domain.Interfaces.Apps.Login;
using ApiSindisure.Domain.Interfaces.Services.Jwt;
using ApiSindisure.Domain.ViewModel.Login;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.Login
{
    public class LoginApp : ILoginApp
    {
        private readonly SupabaseService _supabaseService;
        private readonly IJwtServices _jwtTokenGenerator;

        public LoginApp(SupabaseService supabaseService, IJwtServices jwtTokenGenerator)
        {
            _supabaseService = supabaseService;
            _jwtTokenGenerator = jwtTokenGenerator;
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
                            Provider  = user.AppMetadata?["provider"]?.ToString(),
                            Providers = user.AppMetadata?["providers"] as List<string> 
                                ?? new List<string>()
                        },
                        UserMetadata = new LoginViewModel.UserMetadata
                        {
                            Address        = user.UserMetadata?["address"]?.ToString(),
                            Cep            = user.UserMetadata?["cep"]?.ToString(),
                            City           = user.UserMetadata?["city"]?.ToString(),
                            DateOfBirth    = Convert.ToDateTime(user.UserMetadata?["date_of_birth"]?.ToString()),
                            DocumentNumber = user.UserMetadata?["document_number"]?.ToString(),
                            Email          = user.UserMetadata?["email"]?.ToString(),
                            EmailVerified  = user.UserMetadata?["email_verified"] != null && 
                                            bool.TryParse(user.UserMetadata["email_verified"].ToString(), out var ev) ? ev : false,
                            FirstName      = user.UserMetadata?["first_name"]?.ToString(),
                            LastName       = user.UserMetadata?["last_name"]?.ToString(),
                            Neighborhood   = user.UserMetadata?["neighborhood"]?.ToString(),
                            Number         = user.UserMetadata?["number"]?.ToString(),
                            Phone          = user.UserMetadata?["phone"]?.ToString(),
                            PhoneVerified  = user.UserMetadata?["phone_verified"] != null && 
                                            bool.TryParse(user.UserMetadata["phone_verified"].ToString(), out var pv) ? pv : false,
                            State          = user.UserMetadata?["state"]?.ToString(),
                            Sub            = user.UserMetadata?["sub"]?.ToString(),

                        },
                        Identities = user.Identities?.Select(i => new LoginViewModel.Identity
                        {
                            IdentityId = i.IdentityId,
                            Id = i.Id,
                            UserId = i.UserId,
                            IdentityData = new LoginViewModel.UserMetadata
                            {
                               Address        = i.IdentityData?["address"]?.ToString(),
                                Cep            = i.IdentityData?["cep"]?.ToString(),
                                City           = i.IdentityData?["city"]?.ToString(),
                                DateOfBirth    = Convert.ToDateTime(i.IdentityData?["date_of_birth"]?.ToString()),
                                DocumentNumber = i.IdentityData?["document_number"]?.ToString(),
                                Email          = i.IdentityData?["email"]?.ToString(),
                                EmailVerified  = i.IdentityData?["email_verified"] != null &&
                                                bool.TryParse(i.IdentityData["email_verified"].ToString(), out var iev) ? iev : false,
                                FirstName      = i.IdentityData?["first_name"]?.ToString(),
                                LastName       = i.IdentityData?["last_name"]?.ToString(),
                                Neighborhood   = i.IdentityData?["neighborhood"]?.ToString(),
                                Number         = i.IdentityData?["number"]?.ToString(),
                                Phone          = i.IdentityData?["phone"]?.ToString(),
                                PhoneVerified  = i.IdentityData?["phone_verified"] != null &&
                                                bool.TryParse(i.IdentityData["phone_verified"].ToString(), out var ipv) ? ipv : false,
                                State          = i.IdentityData?["state"]?.ToString(),
                                Sub            = i.IdentityData?["sub"]?.ToString(),

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
                return new LoginViewModel.Response
                {
                    ErrorMessage = "Erro interno do servidor"
                };
            }
        }
    }
}
