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
                    InvoiceNumber = model.InvoiceNumber,
                    Payer = model.Payer,
                    Category = model.Category,
                    Notes = model.Notes,
                    CondominiumId = model.CondominiumId,
                    FileName = model.FileName,
                    FileUrl = model.FileUrl,
                    CreatedAt = model.CreatedAt.ToString("yyyy/MM/dd"),
                    UpdatedAt = model.UpdatedAt.ToString("yyyy/MM/dd")
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
                    InvoiceNumber = request.InvoiceNumber,
                    Payer = request.Payer,
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
                    InvoiceNumber = createdModel.InvoiceNumber,
                    Category = createdModel.Category,
                    Notes = createdModel.Notes,
                    CondominiumId = createdModel.CondominiumId,
                    FileName = createdModel.FileName,
                    FileUrl = createdModel.FileUrl,
                    CreatedAt = createdModel.CreatedAt.ToString("yyyy/MM/dd"),
                    UpdatedAt = createdModel.UpdatedAt.ToString("yyyy/MM/dd")
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar conta a pagar", ex);
            }
        }

        public async Task<List<AccountsReceivableViewModel.Response>> CreateAccountsReceivableBuildingsAsync(List<AccountsReceivableViewModel.CreateRequest> request, CancellationToken cancellationToken)
        {
            try
            {
                var listAccountsReceivableResponse = new List<AccountsReceivableViewModel.Response>();
                var client = _supabaseService.GetClient();

                foreach (var item in request)
                {
                    var model = new AccountsReceivableModel
                    {
                        Description = item.Description,
                        Amount = item.Amount,
                        DueDate = item.DueDate,
                        Status = item.Status,
                        InvoiceNumber = item.InvoiceNumber,
                        Payer = item.Payer,
                        Category = item.Category,
                        Notes = item.Notes,
                        CondominiumId = item.CondominiumId,
                        CreateBy = item.CreateBy,
                        FileName = item.FileName,
                        FileUrl = item.FileUrl,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    var result = await client
                        .From<AccountsReceivableModel>()
                        .Insert(model);

                    var createdModel = result.Models.First();

                    listAccountsReceivableResponse.Add(new AccountsReceivableViewModel.Response
                    {
                        Id = createdModel.Id,
                        Description = createdModel.Description,
                        Amount = createdModel.Amount,
                        DueDate = createdModel.DueDate,
                        Status = createdModel.Status,
                        InvoiceNumber = createdModel.InvoiceNumber,
                        Category = createdModel.Category,
                        Notes = createdModel.Notes,
                        CondominiumId = createdModel.CondominiumId,
                        FileName = createdModel.FileName,
                        FileUrl = createdModel.FileUrl,
                        CreatedAt = createdModel.CreatedAt.ToString("yyyy/MM/dd"),
                        UpdatedAt = createdModel.UpdatedAt.ToString("yyyy/MM/dd")
                    });
                }

                return listAccountsReceivableResponse;
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

                 var resultGet = await client
                    .From<AccountsReceivableModel>()
                    .Select("*")
                    .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();
                
                var retornoGet = resultGet.Models.FirstOrDefault();

                var model = new AccountsReceivableModel
                {
                    Id = request.Id,
                    Description = request.Description,
                    CondominiumId = retornoGet.CondominiumId,
                    Amount = request.Amount,
                    DueDate = request.DueDate,
                    Status = request.Status,
                    InvoiceNumber = request.InvoiceNumber,
                    Payer = request.Payer,
                    Category = request.Category,
                    Notes = request.Notes,
                    FileName = request.FileName,
                    FileUrl = request.FileUrl,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = retornoGet.CreatedAt,
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
                    InvoiceNumber = updatedModel.InvoiceNumber,
                    Category = updatedModel.Category,
                    Notes = updatedModel.Notes,
                    CreateBy = updatedModel.CreateBy,
                    CondominiumId = updatedModel.CondominiumId,
                    FileName = updatedModel.FileName,
                    FileUrl = updatedModel.FileUrl,
                    CreatedAt = updatedModel.CreatedAt.ToString("yyyy/MM/dd"),
                    UpdatedAt = updatedModel.UpdatedAt.ToString("yyyy/MM/dd")
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar conta a pagar", ex);
            }
        }

        public async Task<List<AccountsReceivableViewModel.Response>> UpdateAccountsReceivablePendingFeesAsync(AccountsReceivableViewModel.UpdateManyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();

                // Buscar todas as contas a receber com status "pending" para o condomínio
                var existingFees = await client
                    .From<AccountsReceivableModel>()
                    .Where(x => x.CondominiumId == request.CondominiumId)
                    .Filter("status", Supabase.Postgrest.Constants.Operator.Equals, "pending")
                    .Get();

                var updatedResults = new List<AccountsReceivableViewModel.Response>();

                foreach (var fee in existingFees.Models)
                {
                    // Buscar se temos update para essa fee específica
                    var updateData = request.Updates.FirstOrDefault(u => u.Id == fee.Id);
                    if (updateData == null)
                        continue;

                    var currentDate = fee.DueDate;

                    var newDueDate = new DateTime(
                        currentDate.Year,
                        currentDate.Month,
                        updateData.DueDay
                    );

                    fee.Amount = updateData.Amount;
                    fee.DueDate = newDueDate;
                    fee.Notes = updateData.Notes;
                    fee.UpdatedAt = DateTime.UtcNow;

                    var updateResult = await client
                        .From<AccountsReceivableModel>()
                        .Where(x => x.Id == fee.Id)
                        .Update(fee);

                    var updated = updateResult.Models.First();

                    updatedResults.Add(new AccountsReceivableViewModel.Response
                    {
                        Id = updated.Id,
                        Description = updated.Description,
                        Amount = updated.Amount,
                        DueDate = updated.DueDate,
                        Status = updated.Status,
                        InvoiceNumber = updated.InvoiceNumber,
                        Category = updated.Category,
                        Payer = updated.Payer,
                        Notes = updated.Notes,
                        CreateBy = updated.CreateBy,
                        CondominiumId = updated.CondominiumId,
                        FileName = updated.FileName,
                        FileUrl = updated.FileUrl,
                        CreatedAt = updated.CreatedAt.ToString("yyyy/MM/dd"),
                        UpdatedAt = updated.UpdatedAt.ToString("yyyy/MM/dd")
                    });
                }

                return updatedResults; 
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
        
        public async Task DeleteAccountsReceivablePendingAsync(AccountsReceivableViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<AccountsReceivableModel>()
                    .Where(x => x.CondominiumId == request.Id)
                    .Filter("status", Supabase.Postgrest.Constants.Operator.Equals, "pending")
                    .Delete();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir conta a pagar", ex);
            }
        }
    }    
}

