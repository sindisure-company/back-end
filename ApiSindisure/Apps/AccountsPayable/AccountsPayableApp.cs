using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.AccountsPayable;
using ApiSindisure.Domain.ViewModel.AccountsPayable;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.AccountsPayable
{
    public class AccountsPayableApp : IAccountsPayableApp
    {
        private readonly SupabaseService _supabaseService;

        public AccountsPayableApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<AccountsPayableViewModel.Response>> GetAccountsPayableAsync(AccountsPayableViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("CondominiumId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("CondominiumId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<AccountsPayableModel>()
                    .Select("*")
                    .Filter("condominium_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("due_date", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new AccountsPayableViewModel.Response
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

        public async Task<AccountsPayableViewModel.Response> CreateAccountsPayableAsync(AccountsPayableViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new AccountsPayableModel
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
                    FileName = request.FileName,
                    FileUrl = request.FileUrl,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<AccountsPayableModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new AccountsPayableViewModel.Response
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

        public async Task<AccountsPayableViewModel.Response> UpdateAccountsPayableAsync(AccountsPayableViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new AccountsPayableModel
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
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<AccountsPayableModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new AccountsPayableViewModel.Response
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

        public async Task DeleteAccountsPayableAsync(AccountsPayableViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<AccountsPayableModel>()
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

