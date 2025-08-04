using ApiSindisure.Domain.Interfaces.Apps.AccountsReceivable;
using ApiSindisure.Domain.ViewModel.AccountsReceivable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AccountsReceivableController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<AccountsReceivableViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountsReceivable(
            [FromQuery] string condominiumId,
            [FromServices] IAccountsReceivableApp app)
        {
            try
            {
                if (string.IsNullOrEmpty(condominiumId))
                    return BadRequest(new { error = "CondominiumId não pode ser nulo ou vazio." });

                var request = new AccountsReceivableViewModel.GetRequest { Id = condominiumId };
                var response = await app.GetAccountsReceivableAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar contas a receber. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<AccountsReceivableViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccountsReceivable(
            [FromBody] AccountsReceivableViewModel.CreateRequest request,
            [FromServices] IAccountsReceivableApp app)
        {
            try
            {
                var response = await app.CreateAccountsReceivableAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar conta a pagar. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<AccountsReceivableViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAccountsReceivable(
            string id,
            [FromBody] AccountsReceivableViewModel.UpdateRequest request,
            [FromServices] IAccountsReceivableApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateAccountsReceivableAsync(request, CancellationToken.None);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao atualizar conta a pagar. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("UpdateAccountsReceivablePendingFees")]
        [ProducesResponseType<AccountsReceivableViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAccountsReceivablePendingFees(
            [FromBody] AccountsReceivableViewModel.UpdateManyRequest request,
            [FromServices] IAccountsReceivableApp app)
        {
            try
            {
                var response = await app.UpdateAccountsReceivablePendingFeesAsync(request, CancellationToken.None);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao atualizar conta a pagar. Mais detalhes: " + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAccountsReceivable(
            string id,
            [FromServices] IAccountsReceivableApp app)
        {
            try
            {
                var request = new AccountsReceivableViewModel.DeleteRequest { Id = id };
                await app.DeleteAccountsReceivableAsync(request, CancellationToken.None);
                return Ok(new { message = "Conta a pagar excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir conta a pagar. Mais detalhes: " + ex.Message });
            }
        }
        
        [HttpDelete("DeleteAccountsReceivablePending/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAccountsReceivablePending(
            string id,
            [FromServices] IAccountsReceivableApp app)
        {
            try
            {
                var request = new AccountsReceivableViewModel.DeleteRequest { Id = id };
                await app.DeleteAccountsReceivablePendingAsync(request, CancellationToken.None);
                return Ok(new { message = "Conta a pagar excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir conta a pagar. Mais detalhes: " + ex.Message });
            }
        }
    }
}

