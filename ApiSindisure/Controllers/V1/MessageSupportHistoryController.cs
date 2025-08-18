using ApiSindisure.Domain.Interfaces.Apps.MessageSupportHistory;
using ApiSindisure.Domain.ViewModel.MessageSupportHistoryViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class MessageSupportHistoryController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<MessageSupportHistoryViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMessageSupportHistory(            
            [FromServices] IMessageSupportHistoryApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var id = JwtHelper.GetSubFromUserMetadata(token);
                if (string.IsNullOrEmpty(id))
                    return BadRequest(new { error = "ID do usuário não encontrado no token." });      
                
                var request = new MessageSupportHistoryViewModel.GetRequest { Id = id};
                var response = await app.GetMessageSupportHistoryAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpGet("GetMessageUniqueupportHistory/{id}")]
        [ProducesResponseType<List<MessageSupportHistoryViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMessageUniqueupportHistory(   
            string id,        
            [FromServices] IMessageSupportHistoryApp app)
        {
            try
            {                
                var request = new MessageSupportHistoryViewModel.GetRequest { Id = id};
                var response = await app.GetMessageUniqueSupportHistoryAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<MessageSupportHistoryViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMessageSupportHistory(
            [FromBody] MessageSupportHistoryViewModel.CreateRequest request,
            [FromServices] IMessageSupportHistoryApp app)
        {
            try
            {
                var response = await app.CreateMessageSupportHistoryAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<MessageSupportHistoryViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMessageSupportHistory(
            string id,
            [FromBody] MessageSupportHistoryViewModel.UpdateRequest request,
            [FromServices] IMessageSupportHistoryApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateMessageSupportHistoryAsync(request, CancellationToken.None);
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
        public async Task<IActionResult> DeleteMessageSupportHistory(
            string id,
            [FromServices] IMessageSupportHistoryApp app)
        {
            try
            {
                var request = new MessageSupportHistoryViewModel.DeleteRequest { Id = id };
                await app.DeleteMessageSupportHistoryAsync(request, CancellationToken.None);
                return Ok(new { message = "fornecedor excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir fornecedor. Mais detalhes: " + ex.Message });
            }
        }
    }
}

