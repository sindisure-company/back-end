using ApiSindisure.Domain.Interfaces.Apps.Login;
using ApiSindisure.Domain.ViewModel.Login;
using ApiSindisure.Domain.ViewModel.UserRegisterViewModel;
using ApiSindisure.Filters.Login;
using Microsoft.AspNetCore.Mvc;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AutenticationController : ControllerBase
    {
        /// <summary>
        /// Deve enviar a requisiçao para o Serviço de validação para realizar o login do usuario de acordo com cpf, senha informado
        /// <param name="request">LoginViewModel.Request</param>
        /// <param name="app"></param>
        /// <param name="cancellationToken">CancellationToken</param>
        [HttpPost("login")]
        [ProducesResponseType<LoginViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter<LoginHeadersFilter>]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel.Request loginRequest,
            [FromServices] ILoginApp app)
        {
            var resposta = await app.HandleAsync(loginRequest, CancellationToken.None);
            return Ok(resposta);
        }

        [HttpPost("SignUp")]
        [ProducesResponseType<UserRegisterViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter<LoginHeadersFilter>]
        public async Task<IActionResult> SignUp(
            [FromBody] UserRegisterViewModel.CreateRequest request,
            [FromServices] ILoginApp app)
        {            
            var resposta = await app.SignUp(request, CancellationToken.None);
            return Ok(resposta);
        }
    }
}
