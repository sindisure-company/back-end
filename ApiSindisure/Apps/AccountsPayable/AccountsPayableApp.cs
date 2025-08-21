using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.AccountsPayable;
using ApiSindisure.Domain.ViewModel.AccountsPayable;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.AccountsPayable
{
    public class AccountsPayableApp : IAccountsPayableApp
    {
        private readonly SupabaseService _supabaseService;
        private readonly ILogger<AccountsPayableApp> _logger;
        public string LogId { get; set; }

        public AccountsPayableApp(SupabaseService supabaseService, ILogger<AccountsPayableApp> logger)
        {
            _supabaseService = supabaseService;
            _logger = logger;
            LogId = Guid.NewGuid().ToString();
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
                    CreateBy = model.CreateBy,
                    InvoiceNumber = model.InvoiceNumber,
                    Category = model.Category,
                    Notes = model.Notes,
                    CondominiumId = model.CondominiumId,
                    CompaniesRecurringId = model.CompaniesRecurringId,
                    FileName = model.FileName,
                    FileUrl = model.FileUrl,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AccountsPayableApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<List<AccountsPayableViewModel.Response>> GetAccountsPayablePendingRecurringAsync(AccountsPayableViewModel.GetRequest request, CancellationToken cancellationToken)
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
                    .Filter("companies_recurring_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Filter("status", Supabase.Postgrest.Constants.Operator.Equals, "pending")
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
                    CompaniesRecurringId = model.CompaniesRecurringId,
                    FileName = model.FileName,
                    FileUrl = model.FileUrl,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AccountsPayableApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<List<AccountsPayableViewModel.Response>> GetUpcommingAccountsPayableAsync(AccountsPayableViewModel.GetRequest request, CancellationToken cancellationToken)
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
                    .Not("companies_recurring_id", Supabase.Postgrest.Constants.Operator.Is, "null")
                    .Order("due_date", Supabase.Postgrest.Constants.Ordering.Descending)
                    .Get();


                return result.Models.Select(model => new AccountsPayableViewModel.Response
                {
                    Id = model.Id,
                    Description = model.Description,
                    Amount = model.Amount,
                    DueDate = model.DueDate,
                    Status = model.Status,
                    CreateBy = model.CreateBy,
                    Company = model.Company,
                    InvoiceNumber = model.InvoiceNumber,
                    Category = model.Category,
                    Notes = model.Notes,
                    CondominiumId = model.CondominiumId,
                    CompaniesRecurringId = model.CompaniesRecurringId,
                    FileName = model.FileName,
                    FileUrl = model.FileUrl,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AccountsPayableApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
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
                    CompaniesRecurringId = request.CompaniesRecurringId,
                    CreateBy = request.CreateBy,
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
                _logger.LogError($"{nameof(AccountsPayableApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao criar conta a pagar", ex);
            }
        }

        public async Task<List<AccountsPayableViewModel.Response>> CreateRecurringAccountsPayableAsync(List<AccountsPayableViewModel.CreateRequest> request, CancellationToken cancellationToken)
        {
            try
            {
                var listPayables = new List<AccountsPayableViewModel.Response>();
                var client = _supabaseService.GetClient();

                foreach (var item in request)
                {
                    var model = new AccountsPayableModel
                    {
                        Description = item.Description,
                        Amount = item.Amount,
                        DueDate = item.DueDate,
                        Status = item.Status,
                        Company = item.Company,
                        InvoiceNumber = item.InvoiceNumber,
                        Category = item.Category,
                        Notes = item.Notes,
                        CondominiumId = item.CondominiumId,
                        CompaniesRecurringId = item.CompaniesRecurringId,
                        CreateBy = item.CreateBy,
                        FileName = item.FileName,
                        FileUrl = item.FileUrl,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    var result = await client
                        .From<AccountsPayableModel>()
                        .Insert(model);

                    var createdModel = result.Models.First();
                    listPayables.Add(new AccountsPayableViewModel.Response
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
                    });
                }

                return listPayables;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AccountsPayableApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao criar conta a pagar", ex);
            }
        }

        public async Task<AccountsPayableViewModel.Response> UpdateAccountsPayableAsync(AccountsPayableViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();

                var resultGet = await client
                    .From<AccountsPayableModel>()
                    .Select("*")
                    .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();
                
                var retornoGet = resultGet.Models.FirstOrDefault();

                var model = new AccountsPayableModel
                {
                    Id = request.Id,
                    Description = request.Description,
                    Amount = request.Amount,
                    DueDate = request.DueDate,
                    Status = request.Status,
                    Company = request.Company,
                    InvoiceNumber = request.InvoiceNumber,
                    CompaniesRecurringId = request.CompaniesRecurringId,
                    CondominiumId = request.CondominiumId,
                    Category = request.Category,
                    Notes = request.Notes,
                    FileName = request.FileName,
                    FileUrl = request.FileUrl,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = retornoGet.CreatedAt,
                    CreateBy = request.CreateBy,
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
                _logger.LogError($"{nameof(AccountsPayableApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao atualizar conta a pagar", ex);
            }
        }

        public async Task<AccountsPayableViewModel.Response> UpdateAccountsPayableStatusAsync(AccountsPayableViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new AccountsPayableModel
                {
                    Id = request.Id,
                    Status = request.Status,
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
                    CreateBy = updatedModel.CreateBy,
                    CondominiumId = updatedModel.CondominiumId,
                    CompaniesRecurringId = updatedModel.CompaniesRecurringId,
                    FileName = updatedModel.FileName,
                    FileUrl = updatedModel.FileUrl,
                    CreatedAt = updatedModel.CreatedAt,
                    UpdatedAt = updatedModel.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AccountsPayableApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao atualizar conta a pagar", ex);
            }
        }

        public async Task<List<AccountsPayableViewModel.Response>> UpdateFutureAccountsPayableAsync(List<AccountsPayableViewModel.UpdateRequest> request, CancellationToken cancellationToken)
        {
            try
            {
                var listResult = new List<AccountsPayableViewModel.Response>();
                var client = _supabaseService.GetClient();

                foreach (var item in request)
                {
                     var resultGet = await client
                    .From<AccountsPayableModel>()
                    .Select("*")
                    .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, item.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();
                
                    var retornoGet = resultGet.Models.FirstOrDefault();

                    var model = new AccountsPayableModel
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Amount = item.Amount,
                        DueDate = item.DueDate,
                        Status = item.Status,
                        Company = item.Company,
                        InvoiceNumber = item.InvoiceNumber,
                        Category = item.Category,
                        Notes = item.Notes,
                        CreateBy = item.CreateBy,
                        CondominiumId = item.CondominiumId,
                        CompaniesRecurringId = item.CompaniesRecurringId,
                        FileName = item.FileName,
                        FileUrl = item.FileUrl,
                        UpdatedAt = item.UpdatedAt,
                        CreatedAt = retornoGet.CreatedAt
                    };

                    var result = await client
                        .From<AccountsPayableModel>()
                        .Where(x => x.Id == item.Id)
                        .Update(model);

                    var updatedModel = result.Models.First();

                    var objAccountsPayable = new AccountsPayableViewModel.Response
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
                        CompaniesRecurringId = updatedModel.CompaniesRecurringId,
                        FileName = updatedModel.FileName,
                        FileUrl = updatedModel.FileUrl,
                        CreatedAt = updatedModel.CreatedAt,
                        UpdatedAt = updatedModel.UpdatedAt
                    };

                    listResult.Add(objAccountsPayable);
                }

                return listResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AccountsPayableApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
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
                _logger.LogError($"{nameof(AccountsPayableApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao excluir conta a pagar", ex);
            }
        }
        
        public async Task DeleteUpcommingAccountsPayableAsync(AccountsPayableViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {           
                var client = _supabaseService.GetClient();
                
                await client
                    .From<AccountsPayableModel>()
                    .Where(x => x.CondominiumId == request.Id)
                    .Not("companies_recurring_id", Supabase.Postgrest.Constants.Operator.Is, "null")
                    .Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(AccountsPayableApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao excluir conta a pagar", ex);
            }
        }
    }    
}

