using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.NotificationHistory;
using ApiSindisure.Domain.ViewModel.NotificationHistoryViewModel;
using ApiSindisure.Services.Supabase;

using Supabase.Postgrest.Attributes;

namespace ApiSindisure.Apps.NotificationHistory
{
    public class NotificationHistoryApp : INotificationHistoryApp
    {
        private readonly SupabaseService _supabaseService;

        public NotificationHistoryApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<NotificationHistoryViewModel.Response>> GetNotificationHistoryAsync(NotificationHistoryViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("NotificationHistoryId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("NotificationHistoryId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<NotificationHistoryModel>()
                    .Select("*")
                    .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)                    
                    .Get();

                return result.Models.Select(model => new NotificationHistoryViewModel.Response
                {
                    Id = model.Id,
                    NotificationKey = model.NotificationKey,
                    NotificationType = model.NotificationType,                 
                    ViewedAt = model.ViewedAt,
                    UserId = model.UserId,
                    CreatedAt = model.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<NotificationHistoryViewModel.Response> CreateNotificationHistoryAsync(NotificationHistoryViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var client = _supabaseService.GetClient();
                var model = new NotificationHistoryModel
                {
                    Id = Guid.NewGuid().ToString(),
                    NotificationKey = request.NotificationKey,
                    NotificationType = request.NotificationType,                 
                    ViewedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };

                var result = await client
                    .From<NotificationHistoryModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new NotificationHistoryViewModel.Response
                {
                    Id = createdModel.Id,
                    NotificationKey = createdModel.NotificationKey,
                    NotificationType = createdModel.NotificationType,
                    ViewedAt = createdModel.ViewedAt,
                    CreatedAt = createdModel.CreatedAt,
                    UserId = createdModel.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<NotificationHistoryViewModel.Response> UpdateNotificationHistoryAsync(NotificationHistoryViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new NotificationHistoryModel
                {
                    Id = request.Id,
                    NotificationKey = request.NotificationKey,
                    NotificationType = request.NotificationType,
                    ViewedAt = request.ViewedAt,
                    UserId = request.UserId
                };               

                var result = await client
                    .From<NotificationHistoryModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new NotificationHistoryViewModel.Response
                {
                    Id = updatedModel.Id,
                    NotificationKey = updatedModel.NotificationKey,
                    NotificationType = updatedModel.NotificationType,
                    ViewedAt = updatedModel.ViewedAt,
                    UserId = updatedModel.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteNotificationHistoryAsync(NotificationHistoryViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<NotificationHistoryModel>()
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

