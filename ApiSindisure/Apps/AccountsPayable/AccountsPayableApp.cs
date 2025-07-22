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
                var client = _supabaseService.GetClient();
                var result = await client
                    .From<AccountsPayableModel>()
                    .Select("*")
                    .Where(x => x.CondominiumId == request.CondominiumId)
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

    // Modelo para mapeamento com Supabase
    [Supabase.Postgrest.Attributes.Table("accounts_payable")]
    public class AccountsPayableModel : Supabase.Postgrest.Models.BaseModel
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public string Id { get; set; }

        [Supabase.Postgrest.Attributes.Column("description")]
        public string Description { get; set; }

        [Supabase.Postgrest.Attributes.Column("amount")]
        public decimal Amount { get; set; }

        [Supabase.Postgrest.Attributes.Column("due_date")]
        public DateTime DueDate { get; set; }

        [Supabase.Postgrest.Attributes.Column("status")]
        public string Status { get; set; }

        [Supabase.Postgrest.Attributes.Column("company")]
        public string? Company { get; set; }

        [Supabase.Postgrest.Attributes.Column("invoice_number")]
        public string? InvoiceNumber { get; set; }

        [Supabase.Postgrest.Attributes.Column("category")]
        public string Category { get; set; }

        [Supabase.Postgrest.Attributes.Column("notes")]
        public string? Notes { get; set; }

        [Supabase.Postgrest.Attributes.Column("condominium_id")]
        public string CondominiumId { get; set; }

        [Supabase.Postgrest.Attributes.Column("file_name")]
        public string? FileName { get; set; }

        [Supabase.Postgrest.Attributes.Column("file_url")]
        public string? FileUrl { get; set; }

        [Supabase.Postgrest.Attributes.Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Supabase.Postgrest.Attributes.Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}

