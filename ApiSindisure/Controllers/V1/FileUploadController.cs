using ApiSindisure.Domain.Interfaces.Apps.FileUpload;
using ApiSindisure.Domain.ViewModel.FileUpload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class FileUploadController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FileUpload(
            [FromForm] FileUploadViewModel.Request request,
            [FromServices] IFileUploadApp app)
        {
            try
            {
                var response = await app.FileUploadAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao registrar FileUpload", ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType<List<FileUploadViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DownloadFileUpload(
            [FromQuery] string filePath,
            [FromServices] IFileUploadApp app)
        {
            try
            {
                var request = new FileUploadViewModel.Download { FilePath = filePath };
                var response = await app.DownloadFileAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar logs de FileUploadoria" });
            }
        }
        
        [HttpDelete]
        [ProducesResponseType<FileUploadViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteFileUpload(
            [FromQuery] string filePath,
            [FromServices] IFileUploadApp app)
        {
            try
            {
                var request = new FileUploadViewModel.Delete { FilePath = filePath };
                var response = await app.DeleteFileAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar logs de FileUploadoria" });
            }
        }
    }
}

