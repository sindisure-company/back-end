using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.UserDetails;
using ApiSindisure.Domain.ViewModel.UserDetailsViewModel;
using ApiSindisure.Services.Supabase;

using Supabase.Postgrest.Attributes;

namespace ApiSindisure.Apps.UserDetails
{
    public class UserDetailsApp : IUserDetailsApp
    {
        private readonly SupabaseService _supabaseService;

        public UserDetailsApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<UserDetailsViewModel.Response> GetUserDetailsAsync(UserDetailsViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("UserDetailsId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("UserDetailsId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<UserDetailsModel>()
                    .Select("*")
                    .Filter("auth_user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id.Trim())
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                var model = result.Models.FirstOrDefault();

                return new UserDetailsViewModel.Response
                {
                    Id = model.Id,
                    AuthUserId = model.AuthUserId,
                    Email = model.Email,
                    Address = model.Address,
                    AvatarUrl = model.AvatarUrl,
                    City = model.City,
                    DocumentNumber = model.DocumentNumber,
                    FileName = model.FileName,
                    FileUrl = model.FileUrl,
                    FirstName = model.FirstName,
                    ImgAvatar = model.ImgAvatar,
                    LastName = model.LastName,
                    Neighborhood = model.Neighborhood,
                    Number = model.Number,
                    Phone = model.Phone,
                    State = model.State,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,

                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<UserDetailsViewModel.Response> CreateUserDetailsAsync(UserDetailsViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var client = _supabaseService.GetClient();
                var model = new UserDetailsModel
                {
                    Id = Guid.NewGuid().ToString(),
                    AuthUserId = request.AuthUserId,
                    Email = request.Email,
                    Address = request.Address,
                    AvatarUrl = request.AvatarUrl,
                    City = request.City,
                    DocumentNumber = request.DocumentNumber,
                    FileName = request.FileName,
                    FileUrl = request.FileUrl,
                    FirstName = request.FirstName,
                    ImgAvatar = request.ImgAvatar,
                    LastName = request.LastName,
                    Neighborhood = request.Neighborhood,
                    Number = request.Number,
                    Phone = request.Phone,
                    State = request.State,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<UserDetailsModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new UserDetailsViewModel.Response
                {
                    Id = createdModel.Id,
                    AuthUserId = createdModel.AuthUserId,
                    Email = createdModel.Email,
                    Address = createdModel.Address,
                    AvatarUrl = createdModel.AvatarUrl,
                    City = createdModel.City,
                    DocumentNumber = createdModel.DocumentNumber,
                    FileName = createdModel.FileName,
                    FileUrl = createdModel.FileUrl,
                    FirstName = createdModel.FirstName,
                    ImgAvatar = createdModel.ImgAvatar,
                    LastName = createdModel.LastName,
                    Neighborhood = createdModel.Neighborhood,
                    Number = createdModel.Number,
                    Phone = createdModel.Phone,
                    State = createdModel.State,
                    CreatedAt = createdModel.CreatedAt,
                    UpdatedAt = createdModel.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<UserDetailsViewModel.Response> UpdateUserDetailsAsync(UserDetailsViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();

                var resultGet = await client
                    .From<UserDetailsModel>()
                    .Select("*")
                    .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();
                
                    var retornoGet = resultGet.Models.FirstOrDefault();


                var model = new UserDetailsModel
                    {
                        Id = request.Id,
                        AuthUserId = request.AuthUserId,
                        Email = request.Email,
                        Address = request.Address,
                        AvatarUrl = request.AvatarUrl,
                        City = request.City,
                        DocumentNumber = request.DocumentNumber,
                        FileName = request.FileName,
                        FileUrl = request.FileUrl,
                        FirstName = request.FirstName,
                        ImgAvatar = request.ImgAvatar,
                        LastName = request.LastName,
                        Neighborhood = request.Neighborhood,
                        Number = request.Number,
                        Phone = request.Phone,
                        State = request.State,
                        UpdatedAt = DateTime.UtcNow,
                        CreatedAt = retornoGet.CreatedAt,
                    };               

                var result = await client
                    .From<UserDetailsModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new UserDetailsViewModel.Response
                {
                    Id = updatedModel.Id,
                    AuthUserId = updatedModel.AuthUserId,
                    Email = updatedModel.Email,
                    Address = updatedModel.Address,
                    AvatarUrl = updatedModel.AvatarUrl,
                    City = updatedModel.City,
                    DocumentNumber = updatedModel.DocumentNumber,
                    FileName = updatedModel.FileName,
                    FileUrl = updatedModel.FileUrl,
                    FirstName = updatedModel.FirstName,
                    ImgAvatar = updatedModel.ImgAvatar,
                    LastName = updatedModel.LastName,
                    Neighborhood = updatedModel.Neighborhood,
                    Number = updatedModel.Number,
                    Phone = updatedModel.Phone,
                    State = updatedModel.State,
                    CreatedAt = updatedModel.CreatedAt,
                    UpdatedAt = updatedModel.UpdatedAt,
                  
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteUserDetailsAsync(UserDetailsViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<UserDetailsModel>()
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

