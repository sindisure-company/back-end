using ApiSindisure.Domain.Interfaces.Apps.EmailReportsHistory;
using ApiSindisure.Domain.ViewModel.EmailReportsHistoryViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class EmailReportsHistoryController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<EmailReportsHistoryViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmailReportsHistory(            
            [FromServices] IEmailReportsHistoryApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var id = JwtHelper.GetSubFromUserMetadata(token);
                if (string.IsNullOrEmpty(id))
                    return BadRequest(new { error = "ID do usuário não encontrado no token." });      
                
                var request = new EmailReportsHistoryViewModel.GetRequest { Id = id};
                var response = await app.GetEmailReportsHistoryAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<EmailReportsHistoryViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmailReportsHistory(
            [FromBody] EmailReportsHistoryViewModel.CreateRequest request,
            [FromServices] IEmailReportsHistoryApp app)
        {
            try
            {
                var response = await app.CreateEmailReportsHistoryAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<EmailReportsHistoryViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmailReportsHistory(
            string id,
            [FromBody] EmailReportsHistoryViewModel.UpdateRequest request,
            [FromServices] IEmailReportsHistoryApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateEmailReportsHistoryAsync(request, CancellationToken.None);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao atualizar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEmailReportsHistory(
            string id,
            [FromServices] IEmailReportsHistoryApp app)
        {
            try
            {
                var request = new EmailReportsHistoryViewModel.DeleteRequest { Id = id };
                await app.DeleteEmailReportsHistoryAsync(request, CancellationToken.None);
                return Ok(new { message = "fornecedor excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir fornecedor. Mais detalhes: " + ex.Message });
            }
        }
    }
}

