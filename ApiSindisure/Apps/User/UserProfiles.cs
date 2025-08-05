using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.UserProfiles;
using ApiSindisure.Domain.ViewModel.UserProfilesViewModel;
using ApiSindisure.Services.Supabase;

using Supabase.Postgrest.Attributes;

namespace ApiSindisure.Apps.UserProfiles
{
    public class UserProfilesApp : IUserProfilesApp
    {
        private readonly SupabaseService _supabaseService;

        public UserProfilesApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<UserProfilesViewModel.Response>> GetUserProfilesAsync(UserProfilesViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("UserProfilesId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("UserProfilesId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<UserProfilesModel>()
                    .Select("*")
                    .Filter("created_by", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new UserProfilesViewModel.Response
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DocumentNumber = model.DocumentNumber,
                    DateOfBirth = model.DateOfBirth,
                    AvatarUrl = model.AvatarUrl,
                    Address = model.Address,
                    Number = model.Number,
                    Neighborhood = model.Neighborhood,
                    City = model.City,
                    State = model.State,
                    Phone = model.Phone,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,
                    UserId = model.UserId
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<UserProfilesViewModel.Response> GetUniqueUserProfilesAsync(UserProfilesViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("UserProfilesId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("UserProfilesId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<UserProfilesModel>()
                    .Select("*")
                    .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                if (result.Models.Count == 0)
                    throw new Exception("Nenhum registro encontrado para o UserPermissionsId fornecido.");

                var resultProfile = result.Models.First();

                return new UserProfilesViewModel.Response
                {
                    Id = resultProfile.Id,
                    FirstName = resultProfile.FirstName,
                    LastName = resultProfile.LastName,
                    DocumentNumber = resultProfile.DocumentNumber,
                    DateOfBirth = resultProfile.DateOfBirth,
                    AvatarUrl = resultProfile.AvatarUrl,
                    Address = resultProfile.Address,
                    Number = resultProfile.Number,
                    Neighborhood = resultProfile.Neighborhood,
                    City = resultProfile.City,
                    State = resultProfile.State,
                    Phone = resultProfile.Phone,
                    CreatedAt = resultProfile.CreatedAt,
                    UpdatedAt = resultProfile.UpdatedAt,
                    UserId = resultProfile.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar profile", ex);
            }
        }

        public async Task<UserProfilesViewModel.Response> CreateUserProfilesAsync(UserProfilesViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new UserProfilesModel
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DocumentNumber = request.DocumentNumber,
                    DateOfBirth = request.DateOfBirth,
                    AvatarUrl = request.AvatarUrl,
                    Address = request.Address,
                    Number = request.Number,
                    Neighborhood = request.Neighborhood,
                    City = request.City,
                    State = request.State,
                    Phone = request.Phone,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };

                var result = await client
                    .From<UserProfilesModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new UserProfilesViewModel.Response
                {
                    Id = createdModel.Id,
                    FirstName = createdModel.FirstName,
                    LastName = createdModel.LastName,
                    DocumentNumber = createdModel.DocumentNumber,
                    DateOfBirth = createdModel.DateOfBirth,
                    AvatarUrl = createdModel.AvatarUrl,
                    Address = createdModel.Address,
                    Number = createdModel.Number,
                    Neighborhood = createdModel.Neighborhood,
                    City = createdModel.City,
                    State = createdModel.State,
                    Phone = createdModel.Phone,
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

        public async Task<UserProfilesViewModel.Response> UpdateUserProfilesAsync(UserProfilesViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new UserProfilesModel
                {
                    Id = request.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DocumentNumber = request.DocumentNumber,
                    DateOfBirth = request.DateOfBirth,
                    AvatarUrl = request.AvatarUrl,
                    Address = request.Address,
                    Number = request.Number,
                    Neighborhood = request.Neighborhood,
                    City = request.City,
                    State = request.State,
                    Phone = request.Phone,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = request.UserId                  
                };               

                var result = await client
                    .From<UserProfilesModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new UserProfilesViewModel.Response
                {
                    Id = updatedModel.Id,
                    FirstName = updatedModel.FirstName,
                    LastName = updatedModel.LastName,
                    DocumentNumber = updatedModel.DocumentNumber,
                    DateOfBirth = updatedModel.DateOfBirth,
                    AvatarUrl = updatedModel.AvatarUrl,
                    Address = updatedModel.Address,
                    Number = updatedModel.Number,
                    Neighborhood = updatedModel.Neighborhood,
                    City = updatedModel.City,
                    State = updatedModel.State,
                    Phone = updatedModel.Phone,
                    CreatedAt = updatedModel.CreatedAt,
                    UpdatedAt = updatedModel.UpdatedAt,
                    UserId = updatedModel.UserId
                  
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteUserProfilesAsync(UserProfilesViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<UserProfilesModel>()
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

