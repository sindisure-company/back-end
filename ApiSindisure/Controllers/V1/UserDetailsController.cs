using ApiSindisure.Domain.Interfaces.Apps.UserDetails;
using ApiSindisure.Domain.ViewModel.UserDetailsViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UserDetailsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<List<UserDetailsViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserDetails(            
            [FromServices] IUserDetailsApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var id = JwtHelper.GetSubFromUserMetadata(token);
                if (string.IsNullOrEmpty(id))
                    return BadRequest(new { error = "ID do usuário não encontrado no token." });      
                
                var request = new UserDetailsViewModel.GetRequest { Id = id};
                var response = await app.GetUserDetailsAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }
        [HttpGet("GetListUserDetails")]
        [ProducesResponseType<List<UserDetailsViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListUserDetails(            
            [FromServices] IUserDetailsApp app)
        {
            try
            { 
                var response = await app.GetListUserDetailsAsync(CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<UserDetailsViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserDetails(
            [FromBody] UserDetailsViewModel.CreateRequest request,
            [FromServices] IUserDetailsApp app)
        {
            try
            {
                var response = await app.CreateUserDetailsAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType<UserDetailsViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserDetails(
            string id,
            [FromBody] UserDetailsViewModel.UpdateRequest request,
            [FromServices] IUserDetailsApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateUserDetailsAsync(request, CancellationToken.None);
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
        public async Task<IActionResult> DeleteUserDetails(
            string id,
            [FromServices] IUserDetailsApp app)
        {
            try
            {
                var request = new UserDetailsViewModel.DeleteRequest { Id = id };
                await app.DeleteUserDetailsAsync(request, CancellationToken.None);
                return Ok(new { message = "fornecedor excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir fornecedor. Mais detalhes: " + ex.Message });
            }
        }
    }
}

