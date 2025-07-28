using ApiSindisure.Domain.Interfaces.Apps.Condominium;
using ApiSindisure.Domain.ViewModel.Condominium;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CondominiumController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<CondominiumViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCondominium(            
            [FromServices] ICondominiumApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var id = JwtHelper.GetSubFromUserMetadata(token);
                if (string.IsNullOrEmpty(id))
                    return BadRequest(new { error = "ID do usuário não encontrado no token." });      
                
                var request = new CondominiumViewModel.GetRequest { Id = id};
                var response = await app.GetCondominiumAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar contas a receber. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<CondominiumViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCondominium(
            [FromBody] CondominiumViewModel.CreateRequest request,
            [FromServices] ICondominiumApp app)
        {
            try
            {
                var response = await app.CreateCondominiumAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar conta a pagar. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<CondominiumViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCondominium(
            string id,
            [FromBody] CondominiumViewModel.UpdateRequest request,
            [FromServices] ICondominiumApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateCondominiumAsync(request, CancellationToken.None);
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
        public async Task<IActionResult> DeleteCondominium(
            string id,
            [FromServices] ICondominiumApp app)
        {
            try
            {
                var request = new CondominiumViewModel.DeleteRequest { Id = id };
                await app.DeleteCondominiumAsync(request, CancellationToken.None);
                return Ok(new { message = "Conta a pagar excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir conta a pagar. Mais detalhes: " + ex.Message });
            }
        }
    }
}

