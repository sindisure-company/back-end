using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.CompaniesRecurring;
using ApiSindisure.Domain.ViewModel.CompaniesRecurringViewModel;
using ApiSindisure.Services.Supabase;

using Supabase.Postgrest.Attributes;

namespace ApiSindisure.Apps.CompaniesRecurring
{
    public class CompaniesRecurringApp : ICompaniesRecurringApp
    {
        private readonly SupabaseService _supabaseService;

        public CompaniesRecurringApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<CompaniesRecurringViewModel.Response>> GetCompaniesRecurringAsync(CompaniesRecurringViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("CompaniesRecurringId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("CompaniesRecurringId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<CompaniesRecurringModel>()
                    .Select("*")
                    .Filter("created_by", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new CompaniesRecurringViewModel.Response
                {
                    Id = model.Id,
                    Amount = model.Amount,
                    Category = model.Category,
                    CondominiumId = model.CondominiumId,
                    Description = model.Description,
                    DueDay = model.DueDay,
                    IsActive = model.IsActive,
                    Notes = model.Notes,
                    RecurrenceType = model.RecurrenceType,  
                    CompanyId = model.CompanyId,
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
        
        public async Task<CompaniesRecurringViewModel.Response> GetUniqueCompaniesRecurringAsync(CompaniesRecurringViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("CompaniesRecurringId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("CompaniesRecurringId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<CompaniesRecurringModel>()
                    .Select("*")
                    .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new CompaniesRecurringViewModel.Response
                {
                    Id = model.Id,
                    CompanyId = model.CompanyId,
                    Amount = model.Amount,
                    Category = model.Category,
                    CondominiumId = model.CondominiumId,
                    Description = model.Description,
                    DueDay = model.DueDay,
                    IsActive = model.IsActive,
                    Notes = model.Notes,
                    RecurrenceType = model.RecurrenceType,  
                    CreatedBy = model.CreatedBy,                 
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt
                }).FirstOrDefault() ;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }
        
        public async Task<List<CompaniesRecurringViewModel.Response>> GetCompaniesRecurringIsActiveAsync(CompaniesRecurringViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("CompaniesRecurringId não pode ser nulo ou vazio.");

                if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("CompaniesRecurringId inválido. Deve ser um UUID.");

                var client = _supabaseService.GetClient();

                var result = await client
                    .From<CompaniesRecurringModel>()
                    .Select("*")
                    .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Filter("is_active", Supabase.Postgrest.Constants.Operator.Is, "true")
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new CompaniesRecurringViewModel.Response
                {
                    Id = model.Id,
                    CompanyId = model.CompanyId,
                    Amount = model.Amount,
                    Category = model.Category,
                    CondominiumId = model.CondominiumId,
                    Description = model.Description,
                    DueDay = model.DueDay,
                    IsActive = model.IsActive,
                    Notes = model.Notes,
                    RecurrenceType = model.RecurrenceType,
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

        public async Task<CompaniesRecurringViewModel.Response> CreateCompaniesRecurringAsync(CompaniesRecurringViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new CompaniesRecurringModel
                {
                    Amount = request.Amount,
                    Category = request.Category,
                    CompanyId = request.CompanyId,
                    CondominiumId = request.CondominiumId,
                    Description = request.Description,
                    DueDay = request.DueDay,
                    IsActive = request.IsActive,
                    Notes = request.Notes,
                    RecurrenceType = request.RecurrenceType,
                    CreatedBy = request.CreatedBy,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<CompaniesRecurringModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new CompaniesRecurringViewModel.Response
                {
                    Id = createdModel.Id,
                    CompanyId = createdModel.CompanyId,
                    Amount = createdModel.Amount,
                    Category = createdModel.Category,
                    CondominiumId = createdModel.CondominiumId,
                    Description = createdModel.Description,
                    DueDay = createdModel.DueDay,
                    IsActive = createdModel.IsActive,
                    Notes = createdModel.Notes,
                    RecurrenceType = createdModel.RecurrenceType,
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

        public async Task<CompaniesRecurringViewModel.Response> UpdateCompaniesRecurringAsync(CompaniesRecurringViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();

                var resultGet = await client
                    .From<CompaniesRecurringModel>()
                    .Select("*")
                    .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                var retornoGet = resultGet.Models.FirstOrDefault();

                var model = new CompaniesRecurringModel
                {
                    Id = request.Id,
                    CompanyId = retornoGet.CompanyId,
                    CondominiumId = retornoGet.CondominiumId,
                    Amount = request.Amount,
                    Category = retornoGet.Category,
                    Description = request.Description,
                    DueDay = request.DueDay,
                    IsActive = request.IsActive,
                    RecurrenceType = retornoGet.RecurrenceType,
                    CreatedBy = retornoGet.CreatedBy, 
                    Notes = request.Notes,
                    UpdatedAt = DateTime.UtcNow
                };               

                var result = await client
                    .From<CompaniesRecurringModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new CompaniesRecurringViewModel.Response
                {
                    Id = updatedModel.Id,
                    CompanyId = updatedModel.CompanyId,
                    Amount = updatedModel.Amount,
                    Category = updatedModel.Category,
                    CondominiumId = updatedModel.CondominiumId,
                    Description = updatedModel.Description,
                    DueDay = updatedModel.DueDay,
                    IsActive = updatedModel.IsActive,
                    Notes = updatedModel.Notes,
                    RecurrenceType = updatedModel.RecurrenceType,
                    CreatedBy = updatedModel.CreatedBy,   
                    UpdatedAt = updatedModel.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteCompaniesRecurringAsync(CompaniesRecurringViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<CompaniesRecurringModel>()
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

