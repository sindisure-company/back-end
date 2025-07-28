using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.Condominium;
using ApiSindisure.Domain.ViewModel.Condominium;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.Condominium
{
    public class CondominiumApp : ICondominiumApp
    {
        private readonly SupabaseService _supabaseService;

        public CondominiumApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<CondominiumViewModel.Response>> GetCondominiumAsync(CondominiumViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("CondominiumId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("CondominiumId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<CondominiumModel>()
                    .Select("*")
                    .Filter("created_by", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new CondominiumViewModel.Response
                {
                    Id = model.Id,
                    Address = model.Address,
                    Email = model.Email,
                    Name = model.Name,
                    Observation = model.Observation,
                    PersonalContact = model.PersonalContact,
                    Phone = model.Phone,
                    Units = model.Units,
                    Status = model.Status,
                    CreatedBy = model.CreatedBy,                 
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<CondominiumViewModel.Response> CreateCondominiumAsync(CondominiumViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var client = _supabaseService.GetClient();
                var model = new CondominiumModel
                {
                    Address = request.Address,
                    Email = request.Email,
                    Name = request.Name,
                    Observation = request.Observation,
                    PersonalContact = request.PersonalContact,
                    Phone = request.Phone,
                    Units = request.Units,
                    Status = request.Status,
                    CreatedBy = request.CreatedBy,                 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<CondominiumModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new CondominiumViewModel.Response
                {
                    Id = createdModel.Id,
                    Address = createdModel.Address,
                    Email = createdModel.Email,
                    Name = createdModel.Name,
                    Observation = createdModel.Observation,
                    PersonalContact = createdModel.PersonalContact,
                    Phone = createdModel.Phone,
                    Units = createdModel.Units,
                    Status = createdModel.Status,
                    CreatedBy = createdModel.CreatedBy,                 
                    CreatedAt = createdModel.CreatedAt,
                    UpdatedAt = createdModel.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<CondominiumViewModel.Response> UpdateCondominiumAsync(CondominiumViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new CondominiumModel
                {
                    Id = request.Id,
                    Address = request.Address,
                    Email = request.Email,
                    Name = request.Name,
                    Observation = request.Observation,
                    PersonalContact = request.PersonalContact,
                    Phone = request.Phone,
                    Units = request.Units,
                    Status = request.Status,
                    CreatedBy = request.CreatedBy,                 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<CondominiumModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new CondominiumViewModel.Response
                {
                    Id = updatedModel.Id,
                    Address = updatedModel.Address,
                    Email = updatedModel.Email,
                    Name = updatedModel.Name,
                    Observation = updatedModel.Observation,
                    PersonalContact = updatedModel.PersonalContact,
                    Phone = updatedModel.Phone,
                    Units = updatedModel.Units,
                    Status = updatedModel.Status,
                    CreatedBy = updatedModel.CreatedBy,                 
                    CreatedAt = updatedModel.CreatedAt,
                    UpdatedAt = updatedModel.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteCondominiumAsync(CondominiumViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<CondominiumModel>()
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

