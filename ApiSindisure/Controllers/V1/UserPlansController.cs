using ApiSindisure.Domain.Interfaces.Apps.UserPlans;
using ApiSindisure.Domain.ViewModel.UserPlansViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiSindisure.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]    
    public class UserPlansController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [ProducesResponseType<List<UserPlansViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserPlans(            
            [FromServices] IUserPlansApp app)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Token não pode ser nulo ou vazio." });

                var id = JwtHelper.GetSubFromUserMetadata(token);
                if (string.IsNullOrEmpty(id))
                    return BadRequest(new { error = "ID do usuário não encontrado no token." });      
                
                var request = new UserPlansViewModel.GetRequest { Id = id};
                var response = await app.GetUserPlansAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar fornecedores. Mais detalhes: " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType<List<UserPlansViewModel.Response>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUniqueUserPlans(  
            string id,          
            [FromServices] IUserPlansApp app)
        {
            try
            {     
                var request = new UserPlansViewModel.GetRequest { Id = id};
                var response = await app.GetUniqueUserPlansAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao buscar planos. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType<UserPlansViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserPlans(
            [FromBody] UserPlansViewModel.CreateRequest request,
            [FromServices] IUserPlansApp app)
        {
            try
            {
                var response = await app.CreateUserPlansAsync(request, CancellationToken.None);
                return Created(response.Id, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao criar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType<UserPlansViewModel.Response>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserPlans(
            string id,
            [FromBody] UserPlansViewModel.UpdateRequest request,
            [FromServices] IUserPlansApp app)
        {
            try
            {
                request.Id = id;
                var response = await app.UpdateUserPlansAsync(request, CancellationToken.None);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao atualizar fornecedor. Mais detalhes: " + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUserPlans(
            string id,
            [FromServices] IUserPlansApp app)
        {
            try
            {
                var request = new UserPlansViewModel.DeleteRequest { Id = id };
                await app.DeleteUserPlansAsync(request, CancellationToken.None);
                return Ok(new { message = "fornecedor excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Erro ao excluir fornecedor. Mais detalhes: " + ex.Message });
            }
        }
    }
}

