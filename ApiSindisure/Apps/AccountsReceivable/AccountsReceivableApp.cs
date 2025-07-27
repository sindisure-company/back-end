using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.AccountsReceivable;
using ApiSindisure.Domain.ViewModel.AccountsReceivable;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.AccountsReceivable
{
    public class AccountsReceivableApp : IAccountsReceivableApp
    {
        private readonly SupabaseService _supabaseService;

        public AccountsReceivableApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<AccountsReceivableViewModel.Response>> GetAccountsReceivableAsync(AccountsReceivableViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("CondominiumId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("CondominiumId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<AccountsReceivableModel>()
                    .Select("*")
                    .Filter("condominium_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("due_date", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new AccountsReceivableViewModel.Response
                {
                    Id = model.Id,
                    Description = model.Description,
                    Amount = model.Amount,
                    DueDate = model.DueDate,
                    Status = model.Status,
                    Company = model.Company,
                    InvoiceNumber = model.InvoiceNumber,
                    Category = model.Category,
                    Notes = model.Notes,
                    CondominiumId = model.CondominiumId,
                    FileName = model.FileName,
                    FileUrl = model.FileUrl,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<AccountsReceivableViewModel.Response> CreateAccountsReceivableAsync(AccountsReceivableViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var client = _supabaseService.GetClient();
                var model = new AccountsReceivableModel
                {
                    Description = request.Description,
                    Amount = request.Amount,
                    DueDate = request.DueDate,
                    Status = request.Status,
                    Company = request.Company,
                    InvoiceNumber = request.InvoiceNumber,
                    Category = request.Category,
                    Notes = request.Notes,
                    CondominiumId = request.CondominiumId,
                    CreateBy = request.CreateBy,
                    FileName = request.FileName,
                    FileUrl = request.FileUrl,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<AccountsReceivableModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new AccountsReceivableViewModel.Response
                {
                    Id = createdModel.Id,
                    Description = createdModel.Description,
                    Amount = createdModel.Amount,
                    DueDate = createdModel.DueDate,
                    Status = createdModel.Status,
                    Company = createdModel.Company,
                    InvoiceNumber = createdModel.InvoiceNumber,
                    Category = createdModel.Category,
                    Notes = createdModel.Notes,
                    CondominiumId = createdModel.CondominiumId,
                    FileName = createdModel.FileName,
                    FileUrl = createdModel.FileUrl,
                    CreatedAt = createdModel.CreatedAt,
                    UpdatedAt = createdModel.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar conta a pagar", ex);
            }
        }

        public async Task<AccountsReceivableViewModel.Response> UpdateAccountsReceivableAsync(AccountsReceivableViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new AccountsReceivableModel
                {
                    Id = request.Id,
                    Description = request.Description,
                    Amount = request.Amount,
                    DueDate = request.DueDate,
                    Status = request.Status,
                    Company = request.Company,
                    InvoiceNumber = request.InvoiceNumber,
                    Category = request.Category,
                    Notes = request.Notes,
                    FileName = request.FileName,
                    FileUrl = request.FileUrl,
                    UpdatedAt = DateTime.UtcNow,
                    CreateBy = request.CreateBy,
                };

                var result = await client
                    .From<AccountsReceivableModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new AccountsReceivableViewModel.Response
                {
                    Id = updatedModel.Id,
                    Description = updatedModel.Description,
                    Amount = updatedModel.Amount,
                    DueDate = updatedModel.DueDate,
                    Status = updatedModel.Status,
                    Company = updatedModel.Company,
                    InvoiceNumber = updatedModel.InvoiceNumber,
                    Category = updatedModel.Category,
                    Notes = updatedModel.Notes,
                    CreateBy = updatedModel.CreateBy,
                    CondominiumId = updatedModel.CondominiumId,
                    FileName = updatedModel.FileName,
                    FileUrl = updatedModel.FileUrl,
                    CreatedAt = updatedModel.CreatedAt,
                    UpdatedAt = updatedModel.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar conta a pagar", ex);
            }
        }

        public async Task DeleteAccountsReceivableAsync(AccountsReceivableViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<AccountsReceivableModel>()
                    .Where(x => x.Id == request.Id)
                    .Delete();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir conta a pagar", ex);
            }
        }
    }    
}

