using ApiSindisure.Domain.Interfaces.Apps.CompaniesRecurring;
using ApiSindisure.Domain.ViewModel.CompaniesRecurringViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CompaniesRecurringController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<CompaniesRecurringViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCompaniesRecurring(            
            [FromServices] ICompaniesRecurringApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var id = JwtHelper.GetSubFromUserMetadata(token);
                if (string.IsNullOrEmpty(id))
                    return BadRequest(new { error = "ID do usuário não encontrado no token." });      
                
                var request = new CompaniesRecurringViewModel.GetRequest { Id = id};
                var response = await app.GetCompaniesRecurringAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType<List<CompaniesRecurringViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUniqueCompaniesRecurring(   
            string id,         
            [FromServices] ICompaniesRecurringApp app)
        {
            try
            {
                var request = new CompaniesRecurringViewModel.GetRequest { Id = id};
                var response = await app.GetUniqueCompaniesRecurringAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpGet("GetCompaniesRecurringIsActive/{id}")]
        [ProducesResponseType<List<CompaniesRecurringViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCompaniesRecurringIsActive(
             string id,
            [FromServices] ICompaniesRecurringApp app)
        {
            try
            {
                var request = new CompaniesRecurringViewModel.GetRequest { Id = id};
                var response = await app.GetCompaniesRecurringIsActiveAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<CompaniesRecurringViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCompaniesRecurring(
            [FromBody] CompaniesRecurringViewModel.CreateRequest request,
            [FromServices] ICompaniesRecurringApp app)
        {
            try
            {
                var response = await app.CreateCompaniesRecurringAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<CompaniesRecurringViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCompaniesRecurring(
            string id,
            [FromBody] CompaniesRecurringViewModel.UpdateRequest request,
            [FromServices] ICompaniesRecurringApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateCompaniesRecurringAsync(request, CancellationToken.None);
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
        public async Task<IActionResult> DeleteCompaniesRecurring(
            string id,
            [FromServices] ICompaniesRecurringApp app)
        {
            try
            {
                var request = new CompaniesRecurringViewModel.DeleteRequest { Id = id };
                await app.DeleteCompaniesRecurringAsync(request, CancellationToken.None);
                return Ok(new { message = "fornecedor excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir fornecedor. Mais detalhes: " + ex.Message });
            }
        }
    }
}

