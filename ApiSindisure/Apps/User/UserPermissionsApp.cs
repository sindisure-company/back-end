using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.UserPermissions;
using ApiSindisure.Domain.ViewModel.UserPermissionsViewModel;
using ApiSindisure.Services.Supabase;

using Supabase.Postgrest.Attributes;

namespace ApiSindisure.Apps.UserPermissions
{
    public class UserPermissionsApp : IUserPermissionsApp
    {
        private readonly SupabaseService _supabaseService;

        public UserPermissionsApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<UserPermissionsViewModel.Response>> GetUserPermissionsAsync(UserPermissionsViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("UserPermissionsId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("UserPermissionsId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<UserPermissionsModel>()
                    .Select("*")
                    .Filter("created_by", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new UserPermissionsViewModel.Response
                {
                    Id = model.Id,
                    Roles = model.Roles,
                    UpdatedAt = model.UpdatedAt,
                    UserId = model.UserId
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }
        
        public async Task<UserPermissionsViewModel.Response> GetUniqueUserPermissionsAsync(UserPermissionsViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("UserPermissionsId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("UserPermissionsId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<UserPermissionsModel>()
                    .Select("*")
                    .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                if (result.Models.Count == 0)
                    throw new Exception("Nenhum registro encontrado para o UserPermissionsId fornecido.");

                var resultRoles = result.Models.First();

                return new UserPermissionsViewModel.Response
                {
                    Id = resultRoles.Id,
                    Roles = resultRoles.Roles,
                    CreatedAt = resultRoles.CreatedAt,
                    UpdatedAt = resultRoles.UpdatedAt,
                    UserId = resultRoles.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<UserPermissionsViewModel.Response> CreateUserPermissionsAsync(UserPermissionsViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new UserPermissionsModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Roles = request.Roles,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };

                var result = await client
                    .From<UserPermissionsModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new UserPermissionsViewModel.Response
                {
                    Id = createdModel.Id,
                    Roles = createdModel.Roles,
                    CreatedAt = createdModel.CreatedAt,
                    UpdatedAt = createdModel.UpdatedAt,
                    UserId = createdModel.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<UserPermissionsViewModel.Response> UpdateUserPermissionsAsync(UserPermissionsViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new UserPermissionsModel
                {
                    Id = request.Id,
                    Roles = request.Roles,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };               

                var result = await client
                    .From<UserPermissionsModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new UserPermissionsViewModel.Response
                {
                    Id = updatedModel.Id,
                    Roles = updatedModel.Roles,
                    UpdatedAt = updatedModel.UpdatedAt,
                    UserId = updatedModel.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteUserPermissionsAsync(UserPermissionsViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<UserPermissionsModel>()
                    .Where(x => x.Id == request.Id)
                    .Delete();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir condominio", ex);
            }
        }
    }    
}

