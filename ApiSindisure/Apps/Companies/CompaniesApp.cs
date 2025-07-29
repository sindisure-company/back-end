using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.Companies;
using ApiSindisure.Domain.ViewModel.CompaniesViewModel;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.Companies
{
    public class CompaniesApp : ICompaniesApp
    {
        private readonly SupabaseService _supabaseService;

        public CompaniesApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<CompaniesViewModel.Response>> GetCompaniesAsync(CompaniesViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("CompaniesId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("CompaniesId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<CompaniesModel>()
                    .Select("*")
                    .Filter("created_by", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new CompaniesViewModel.Response
                {
                    Id = model.Id,
                    Address = model.Address,
                    Category = model.Category,
                    Document = model.Document,
                    Email = model.Email,
                    Name = model.Name,
                    Notes = model.Notes,
                    PersonalContact = model.PersonalContact,
                    Phone = model.Phone,                    
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

        public async Task<CompaniesViewModel.Response> CreateCompaniesAsync(CompaniesViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var client = _supabaseService.GetClient();
                var model = new CompaniesModel
                {
                    Address = request.Address,
                    Category = request.Category,
                    Document = request.Document,
                    Email = request.Email,
                    Name = request.Name,
                    Notes = request.Notes,
                    PersonalContact = request.PersonalContact,
                    Phone = request.Phone,                    
                    Status = request.Status,
                    CreatedBy = request.CreatedBy,                 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<CompaniesModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new CompaniesViewModel.Response
                {
                    Id = createdModel.Id,
                    Address = createdModel.Address,
                    Category = createdModel.Category,
                    Document = createdModel.Document,
                    Email = createdModel.Email,
                    Name = createdModel.Name,
                    Notes = createdModel.Notes,
                    PersonalContact = createdModel.PersonalContact,
                    Phone = createdModel.Phone,                    
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

        public async Task<CompaniesViewModel.Response> UpdateCompaniesAsync(CompaniesViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new CompaniesModel
                {
                    Address = request.Address,
                    Category = request.Category,
                    Document = request.Document,
                    Email = request.Email,
                    Name = request.Name,
                    Notes = request.Notes,
                    PersonalContact = request.PersonalContact,
                    Phone = request.Phone,                    
                    Status = request.Status,                  
                    CreatedBy = request.CreatedBy,                 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<CompaniesModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new CompaniesViewModel.Response
                {
                    Id = updatedModel.Id,
                    Address = updatedModel.Address,
                    Category = updatedModel.Category,
                    Document = updatedModel.Document,
                    Email = updatedModel.Email,
                    Name = updatedModel.Name,
                    Notes = updatedModel.Notes,
                    PersonalContact = updatedModel.PersonalContact,
                    Phone = updatedModel.Phone,                    
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

        public async Task DeleteCompaniesAsync(CompaniesViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<CompaniesModel>()
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

