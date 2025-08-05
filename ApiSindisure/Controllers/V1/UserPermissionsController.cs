using ApiSindisure.Domain.Interfaces.Apps.UserPermissions;
using ApiSindisure.Domain.ViewModel.UserPermissionsViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UserPermissionsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<UserPermissionsViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserPermissions(            
            [FromServices] IUserPermissionsApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var id = JwtHelper.GetSubFromUserMetadata(token);
                if (string.IsNullOrEmpty(id))
                    return BadRequest(new { error = "ID do usuário não encontrado no token." });      
                
                var request = new UserPermissionsViewModel.GetRequest { Id = id};
                var response = await app.GetUserPermissionsAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType<List<UserPermissionsViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUniqueUserPermissions( 
            string id,           
            [FromServices] IUserPermissionsApp app)
        {
            try
            {
                var request = new UserPermissionsViewModel.GetRequest { Id = id};
                var response = await app.GetUniqueUserPermissionsAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<UserPermissionsViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserPermissions(
            [FromBody] UserPermissionsViewModel.CreateRequest request,
            [FromServices] IUserPermissionsApp app)
        {
            try
            {
                var response = await app.CreateUserPermissionsAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<UserPermissionsViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserPermissions(
            string id,
            [FromBody] UserPermissionsViewModel.UpdateRequest request,
            [FromServices] IUserPermissionsApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateUserPermissionsAsync(request, CancellationToken.None);
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
        public async Task<IActionResult> DeleteUserPermissions(
            string id,
            [FromServices] IUserPermissionsApp app)
        {
            try
            {
                var request = new UserPermissionsViewModel.DeleteRequest { Id = id };
                await app.DeleteUserPermissionsAsync(request, CancellationToken.None);
                return Ok(new { message = "fornecedor excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir fornecedor. Mais detalhes: " + ex.Message });
            }
        }
    }
}

