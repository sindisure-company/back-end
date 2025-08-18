using ApiSindisure.Domain.Interfaces.Apps.Audit;
using ApiSindisure.Domain.ViewModel.Audit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]    
    public class AuditController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogAudit(
            [FromBody] AuditViewModel.LogRequest request,
            [FromServices] IAuditApp app)
        {
            try
            {
                await app.LogAuditAsync(request, CancellationToken.None);
                return Ok(new { message = "Auditoria registrada com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao registrar auditoria" });
            }
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType<List<AuditViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAuditLogs(
            [FromQuery] string? userId,
            [FromQuery] string? context,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromServices] IAuditApp app)
        {
            try
            {
                var request = new AuditViewModel.GetRequest
                {
                    UserId = userId,
                    Context = context,
                    StartDate = startDate,
                    EndDate = endDate
                };
                
                var response = await app.GetAuditLogsAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar logs de auditoria" });
            }
        }
    }
}

