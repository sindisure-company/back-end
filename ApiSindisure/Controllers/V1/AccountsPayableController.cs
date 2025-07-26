using ApiSindisure.Domain.Interfaces.Apps.AccountsPayable;
using ApiSindisure.Domain.ViewModel.AccountsPayable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AccountsPayableController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<AccountsPayableViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountsPayable(
            [FromQuery] string condominiumId,
            [FromServices] IAccountsPayableApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(condominiumId))
                    return BadRequest(new { error = "CondominiumId não pode ser nulo ou vazio." });
                
                if(string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var request = new AccountsPayableViewModel.GetRequest { Id = condominiumId, Token = token };
                var response = await app.GetAccountsPayableAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar contas a pagar. Mais detalhes: " + ex.Message });
            }
        }

        // [HttpPost]
        // [ProducesResponseType<AccountsPayableViewModel.Response>(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<IActionResult> CreateAccountsPayable(
        //     [FromBody] AccountsPayableViewModel.CreateRequest request,
        //     [FromServices] IAccountsPayableApp app)
        // {
        //     try
        //     {
        //         var response = await app.CreateAccountsPayableAsync(request, CancellationToken.None);
        //         return Ok(response);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { error = "Erro ao criar conta a pagar" });
        //     }
        // }

        // [HttpPut("{id}")]
        // [ProducesResponseType<AccountsPayableViewModel.Response>(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<IActionResult> UpdateAccountsPayable(
        //     string id,
        //     [FromBody] AccountsPayableViewModel.UpdateRequest request,
        //     [FromServices] IAccountsPayableApp app)
        // {
        //     try
        //     {
        //         request.Id = id;
        //         var response = await app.UpdateAccountsPayableAsync(request, CancellationToken.None);
        //         return Ok(response);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { error = "Erro ao atualizar conta a pagar" });
        //     }
        // }

        // [HttpDelete("{id}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<IActionResult> DeleteAccountsPayable(
        //     string id,
        //     [FromServices] IAccountsPayableApp app)
        // {
        //     try
        //     {
        //         var request = new AccountsPayableViewModel.DeleteRequest { Id = id };
        //         await app.DeleteAccountsPayableAsync(request, CancellationToken.None);
        //         return Ok(new { message = "Conta a pagar excluída com sucesso" });
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { error = "Erro ao excluir conta a pagar" });
        //     }
        // }
    }
}

