using ApiSindisure.Domain.Interfaces.Apps.NotificationHistory;
using ApiSindisure.Domain.ViewModel.NotificationHistoryViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class NotificationHistoryController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<NotificationHistoryViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNotificationHistory(            
            [FromServices] INotificationHistoryApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var id = JwtHelper.GetSubFromUserMetadata(token);
                if (string.IsNullOrEmpty(id))
                    return BadRequest(new { error = "ID do usuário não encontrado no token." });      
                
                var request = new NotificationHistoryViewModel.GetRequest { Id = id};
                var response = await app.GetNotificationHistoryAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<NotificationHistoryViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNotificationHistory(
            [FromBody] NotificationHistoryViewModel.CreateRequest request,
            [FromServices] INotificationHistoryApp app)
        {
            try
            {
                var response = await app.CreateNotificationHistoryAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<NotificationHistoryViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNotificationHistory(
            string id,
            [FromBody] NotificationHistoryViewModel.UpdateRequest request,
            [FromServices] INotificationHistoryApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateNotificationHistoryAsync(request, CancellationToken.None);
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
        public async Task<IActionResult> DeleteNotificationHistory(
            string id,
            [FromServices] INotificationHistoryApp app)
        {
            try
            {
                var request = new NotificationHistoryViewModel.DeleteRequest { Id = id };
                await app.DeleteNotificationHistoryAsync(request, CancellationToken.None);
                return Ok(new { message = "fornecedor excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir fornecedor. Mais detalhes: " + ex.Message });
            }
        }
    }
}

