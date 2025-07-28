using ApiSindisure.Domain.Interfaces.Apps.Buildings;
using ApiSindisure.Domain.ViewModel.Buildings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class BuildingsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<BuildingsViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBuildings(
            [FromQuery] string condominiumId,
            [FromServices] IBuildingsApp app)
        {
            try
            {
                if (string.IsNullOrEmpty(condominiumId))
                    return BadRequest(new { error = "CondominiumId não pode ser nulo ou vazio." });
                
                var request = new BuildingsViewModel.GetRequest { Id = condominiumId};
                var response = await app.GetBuildingsAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar contas a pagar. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<BuildingsViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBuildings(
            [FromBody] BuildingsViewModel.CreateRequest request,
            [FromServices] IBuildingsApp app)
        {
            try
            {
                var response = await app.CreateBuildingsAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar conta a pagar. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<BuildingsViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBuildings(
            string id,
            [FromBody] BuildingsViewModel.UpdateRequest request,
            [FromServices] IBuildingsApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateBuildingsAsync(request, CancellationToken.None);
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
        public async Task<IActionResult> DeleteBuildings(
            string id,
            [FromServices] IBuildingsApp app)
        {
            try
            {
                var request = new BuildingsViewModel.DeleteRequest { Id = id };
                await app.DeleteBuildingsAsync(request, CancellationToken.None);
                return Ok(new { message = "Conta a pagar excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir conta a pagar. Mais detalhes: " + ex.Message });
            }
        }
    }
}

