using ApiSindisure.Domain.Interfaces.Apps.EmailAutomation;
using ApiSindisure.Domain.ViewModel.EmailAutomationViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class EmailAutomationController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<EmailAutomationViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmailAutomation(            
            [FromServices] IEmailAutomationApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var id = JwtHelper.GetSubFromUserMetadata(token);
                if (string.IsNullOrEmpty(id))
                    return BadRequest(new { error = "ID do usuário não encontrado no token." });      
                
                var request = new EmailAutomationViewModel.GetRequest { Id = id};
                var response = await app.GetEmailAutomationAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<EmailAutomationViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmailAutomation(
            [FromBody] EmailAutomationViewModel.CreateRequest request,
            [FromServices] IEmailAutomationApp app)
        {
            try
            {
                var response = await app.CreateEmailAutomationAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<EmailAutomationViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmailAutomation(
            string id,
            [FromBody] EmailAutomationViewModel.UpdateRequest request,
            [FromServices] IEmailAutomationApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateEmailAutomationAsync(request, CancellationToken.None);
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
        public async Task<IActionResult> DeleteEmailAutomation(
            string id,
            [FromServices] IEmailAutomationApp app)
        {
            try
            {
                var request = new EmailAutomationViewModel.DeleteRequest { Id = id };
                await app.DeleteEmailAutomationAsync(request, CancellationToken.None);
                return Ok(new { message = "fornecedor excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir fornecedor. Mais detalhes: " + ex.Message });
            }
        }
    }
}

