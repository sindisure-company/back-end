using ApiSindisure.Domain.Interfaces.Apps.Login;
using ApiSindisure.Domain.Interfaces.Apps.Payments;
using ApiSindisure.Domain.ViewModel.Login;
using ApiSindisure.Domain.ViewModel.PaymentsAppViewModel;
using ApiSindisure.Domain.ViewModel.UserRegisterViewModel;
using ApiSindisure.Filters.Login;
using Microsoft.AspNetCore.Mvc;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController : ControllerBase
    {
        /// <summary>
        /// Servico que processa o pagamento com cartao de credito
        /// <param name="request">PaymentsAppViewModel.Request</param>
        /// <param name="app"></param>
        /// <param name="cancellationToken">CancellationToken</param>
        [HttpPost("PayWithCreditCard")]
        [ProducesResponseType<PaymentsAppViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter<LoginHeadersFilter>]
        public async Task<IActionResult> PayWithCreditCard(
            [FromBody] PaymentsAppViewModel.CreateRequest request,
            [FromServices] IPaymentsApp app)
        {
            var resposta = await app.PayWithCreditCardAsync(request, CancellationToken.None);
            return Ok(resposta);
        }

        /// <summary>
        /// Servico que processa o pagamento com pix
        /// <param name="request">PaymentsAppViewModel.Request</param>
        /// <param name="app"></param>
        /// <param name="cancellationToken">CancellationToken</param>
        [HttpPost("PayWithPix")]
        [ProducesResponseType<PaymentsAppViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter<LoginHeadersFilter>]
        public async Task<IActionResult> PayWithPix(
            [FromBody] PaymentsAppViewModel.CreateRequest request,
            [FromServices] IPaymentsApp app)
        {
            var resposta = await app.PayWithPixAsync(request, CancellationToken.None);
            return Ok(resposta);
        }
    }
}
