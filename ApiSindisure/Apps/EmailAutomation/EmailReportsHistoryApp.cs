using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.EmailReportsHistory;
using ApiSindisure.Domain.ViewModel.EmailReportsHistoryViewModel;
using ApiSindisure.Services.Supabase;

using Supabase.Postgrest.Attributes;

namespace ApiSindisure.Apps.EmailReportsHistory
{
    public class EmailReportsHistoryApp : IEmailReportsHistoryApp
    {
        private readonly SupabaseService _supabaseService;

        public EmailReportsHistoryApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<EmailReportsHistoryViewModel.Response>> GetEmailReportsHistoryAsync(EmailReportsHistoryViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("EmailReportsHistoryId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("EmailReportsHistoryId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<EmailReportsHistoryModel>()
                    .Select("*")
                    .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new EmailReportsHistoryViewModel.Response
                {
                    Id = model.Id,
                    AutomationId = model.AutomationId,
                    FileName = model.FileName,                 
                    FileUrl = model.FileUrl,
                    RecipientEmail = model.RecipientEmail,
                    ReportType = model.ReportType,
                    SentAt = model.SentAt,
                    Status = model.Status,
                    UserId = model.UserId,                   
                    CreatedAt = model.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<EmailReportsHistoryViewModel.Response> CreateEmailReportsHistoryAsync(EmailReportsHistoryViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var client = _supabaseService.GetClient();
                var model = new EmailReportsHistoryModel
                {
                    Id = Guid.NewGuid().ToString(),
                    AutomationId = request.AutomationId,
                    FileName = request.FileName,                 
                    FileUrl = request.FileUrl,
                    RecipientEmail = request.RecipientEmail,
                    ReportType = request.ReportType,
                    SentAt = request.SentAt,
                    Status = request.Status,                   
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };

                var result = await client
                    .From<EmailReportsHistoryModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new EmailReportsHistoryViewModel.Response
                {
                    Id = createdModel.Id,
                    AutomationId = createdModel.AutomationId,
                    FileName = createdModel.FileName,
                    FileUrl = createdModel.FileUrl,
                    RecipientEmail = createdModel.RecipientEmail,
                    ReportType = createdModel.ReportType,
                    SentAt = createdModel.SentAt,
                    Status = createdModel.Status,
                    CreatedAt = createdModel.CreatedAt,               
                    UserId = createdModel.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<EmailReportsHistoryViewModel.Response> UpdateEmailReportsHistoryAsync(EmailReportsHistoryViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new EmailReportsHistoryModel
                {
                    Id = request.Id,
                    AutomationId = request.AutomationId,
                    FileName = request.FileName,
                    FileUrl = request.FileUrl,
                    RecipientEmail = request.RecipientEmail ,
                    ReportType = request.ReportType,
                    SentAt = request.SentAt,
                    Status = request.Status,                  
                    UserId = request.UserId
                };               

                var result = await client
                    .From<EmailReportsHistoryModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new EmailReportsHistoryViewModel.Response
                {
                    Id = updatedModel.Id,
                    AutomationId = updatedModel.AutomationId,
                    FileName = updatedModel.FileName,
                    FileUrl = updatedModel.FileUrl,
                    RecipientEmail = updatedModel.RecipientEmail,
                    ReportType = updatedModel.ReportType,
                    SentAt = updatedModel.SentAt,
                    Status = updatedModel.Status,                
                    UserId = updatedModel.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteEmailReportsHistoryAsync(EmailReportsHistoryViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<EmailReportsHistoryModel>()
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

