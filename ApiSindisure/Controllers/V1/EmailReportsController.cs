using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiSindisure.Domain.Interfaces.Apps.EmailReportsApp;
using ApiSindisure.Domain.ViewModel.EmailViewModel;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class EmailReportsController : ControllerBase
    {
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmailReports(
            [FromBody] EmailViewModel.Request request,
            [FromServices] IEmailReportsApp app)
        {
            try
            {
                await app.SendMonthlyReportAsync(request, CancellationToken.None);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

    }
}

