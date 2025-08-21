using ApiSindisure.Domain.Interfaces.Apps.EmailAutomation;
using ApiSindisure.Domain.ViewModel.EmailAutomationViewModel;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.EmailAutomation
{
    public class EmailAutomationApp : IEmailAutomationApp
    {
        private readonly SupabaseService _supabaseService;
        private readonly ILogger<EmailAutomationApp> _logger;
        public string LogId { get; set; }

        public EmailAutomationApp(SupabaseService supabaseService, ILogger<EmailAutomationApp> logger)
        {
            _supabaseService = supabaseService;
            _logger = logger;
            LogId = Guid.NewGuid().ToString();
        }

        public async Task<List<EmailAutomationViewModel.Response>> GetEmailAutomationAsync(EmailAutomationViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("EmailAutomationId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("EmailAutomationId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<EmailAutomationModel>()
                    .Select("*")
                    .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new EmailAutomationViewModel.Response
                {
                    Id = model.Id,
                    Email = model.Email,
                    IsActive = model.IsActive,                 
                    ResponsibleName = model.ResponsibleName,
                    SendCashFlow = model.SendCashFlow,
                    SendFiles = model.SendFiles,
                    UserId = model.UserId,
                    UpdatedAt = model.UpdatedAt,
                    CreatedAt = model.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmailAutomationApp)} - Erro ao acessar o banco de dados: {LogId}" + ex.Message, ex);
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<EmailAutomationViewModel.Response> CreateEmailAutomationAsync(EmailAutomationViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var client = _supabaseService.GetClient();
                var model = new EmailAutomationModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = request.Email,
                    IsActive = request.IsActive,                 
                    ResponsibleName = request.ResponsibleName,
                    SendCashFlow = request.SendCashFlow,
                    SendFiles = request.SendFiles,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };

                var result = await client
                    .From<EmailAutomationModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new EmailAutomationViewModel.Response
                {
                    Id = createdModel.Id,
                    Email = createdModel.Email,
                    IsActive = createdModel.IsActive,
                    ResponsibleName = createdModel.ResponsibleName,
                    SendCashFlow = createdModel.SendCashFlow,
                    SendFiles = createdModel.SendFiles,
                    CreatedAt = createdModel.CreatedAt,
                    UpdatedAt = createdModel.UpdatedAt,
                    UserId = createdModel.UserId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmailAutomationApp)} - Erro ao acessar o banco de dados: {LogId}" + ex.Message, ex);
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<EmailAutomationViewModel.Response> UpdateEmailAutomationAsync(EmailAutomationViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var result = await client
                    .From<EmailAutomationModel>()
                    .Select("*")
                    .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Get();

                var existing = result.Models.FirstOrDefault();

                if (existing == null)
                    throw new Exception("Registro não encontrado");

                var model = new EmailAutomationModel
                {
                    Id = request.Id,
                    Email = request.Email,
                    IsActive = request.IsActive,
                    ResponsibleName = request.ResponsibleName,
                    SendCashFlow = request.SendCashFlow ,
                    SendFiles = request.SendFiles,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = existing.CreatedAt,
                    UserId = request.UserId
                };               

                var resultUpdate = await client
                    .From<EmailAutomationModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = resultUpdate.Models.First();

                return new EmailAutomationViewModel.Response
                {
                    Id = updatedModel.Id,
                    Email = updatedModel.Email,
                    IsActive = updatedModel.IsActive,
                    ResponsibleName = updatedModel.ResponsibleName,
                    SendCashFlow = updatedModel.SendCashFlow,
                    SendFiles = updatedModel.SendFiles,
                    UpdatedAt = updatedModel.UpdatedAt,
                    UserId = updatedModel.UserId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmailAutomationApp)} - Erro ao acessar o banco de dados: {LogId}" + ex.Message, ex);
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteEmailAutomationAsync(EmailAutomationViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<EmailAutomationModel>()
                    .Where(x => x.Id == request.Id)
                    .Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(EmailAutomationApp)} - Erro ao acessar o banco de dados: {LogId}" + ex.Message, ex);
                throw new Exception("Erro ao excluir condominio", ex);
            }
        }
    }    
}

