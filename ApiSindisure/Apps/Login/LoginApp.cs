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

                if (session == null)
                {
                    return new LoginViewModel.Response
                    {
                        Error = "Credenciais inválidas"
                    };
                }

                var user = await _supabaseService.GetUser(session.AccessToken);

                var token = _jwtTokenGenerator.GenerateToken(session.User.Id);
                
                var name = LerTokenJwtHelper.GetFisrtName(session.AccessToken);
                var confirmationMail = LerTokenJwtHelper.GetConfirmationMail(session.AccessToken);

                var response = new LoginViewModel.Response
                {
                    User = new UserDto
                    {
                        Id = session.User.Id,
                        Nome = name,
                        Email = session.User.Email,
                        Email_confirmed_at = confirmationMail
                    },
                    Session = new SessionDto
                    {
                        AccessToken = session.AccessToken,
                        ExpiresAt = DateTime.UtcNow.AddHours(1)
                    },
                    WeakPassword = false,
                    Error = null
                };

                return response;
            }
            catch (Exception ex)
            {
                return new LoginViewModel.Response
                {
                    Error = "Erro interno do servidor"
                };
            }
        }
    }
}
