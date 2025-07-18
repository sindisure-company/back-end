using ApiSindisure.Domain.ViewModel.Login;
using ApiSindisure.Services.Jwt;
using ApiSindisure.Services.Supabase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSindisure.Controllers.V1
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class AutenticationController : ControllerBase
    {
        private readonly SupabaseService _supabaseService;
        private readonly JwtService _jwtTokenGenerator;

        public AutenticationController(SupabaseService supabaseService, JwtService jwtTokenGenerator)
        {
            _supabaseService = supabaseService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            var session = await _supabaseService.SignIn(login.Email, login.Password);
            if (session == null)
                return Unauthorized("Credenciais inválidas");

            var token = _jwtTokenGenerator.GenerateToken(login.Email);
            return Ok(new { Token = token });
        }
    }
}
