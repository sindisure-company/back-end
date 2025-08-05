using ApiSindisure.Domain.Interfaces.Apps.UserPlansHistory;
using ApiSindisure.Domain.ViewModel.UserPlansHistoryViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UserPlansHistoryController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<UserPlansHistoryViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserPlansHistory(            
            [FromServices] IUserPlansHistoryApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var id = JwtHelper.GetSubFromUserMetadata(token);
                if (string.IsNullOrEmpty(id))
                    return BadRequest(new { error = "ID do usuário não encontrado no token." });      
                
                var request = new UserPlansHistoryViewModel.GetRequest { Id = id};
                var response = await app.GetUserPlansHistoryAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<UserPlansHistoryViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserPlansHistory(
            [FromBody] UserPlansHistoryViewModel.CreateRequest request,
            [FromServices] IUserPlansHistoryApp app)
        {
            try
            {
                var response = await app.CreateUserPlansHistoryAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<UserPlansHistoryViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserPlansHistory(
            string id,
            [FromBody] UserPlansHistoryViewModel.UpdateRequest request,
            [FromServices] IUserPlansHistoryApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateUserPlansHistoryAsync(request, CancellationToken.None);
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
        public async Task<IActionResult> DeleteUserPlansHistory(
            string id,
            [FromServices] IUserPlansHistoryApp app)
        {
            try
            {
                var request = new UserPlansHistoryViewModel.DeleteRequest { Id = id };
                await app.DeleteUserPlansHistoryAsync(request, CancellationToken.None);
                return Ok(new { message = "fornecedor excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir fornecedor. Mais detalhes: " + ex.Message });
            }
        }
    }
}

