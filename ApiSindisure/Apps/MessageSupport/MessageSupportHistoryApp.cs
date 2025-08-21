using ApiSindisure.Domain.Interfaces.Apps.MessageSupportHistory;
using ApiSindisure.Domain.ViewModel.MessageSupportHistoryViewModel;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.MessageSupportHistory
{
    public class MessageSupportHistoryApp : IMessageSupportHistoryApp
    {
        private readonly SupabaseService _supabaseService;
        private readonly ILogger<MessageSupportHistoryApp> _logger;
        public string LogId { get; set; }

        public MessageSupportHistoryApp(SupabaseService supabaseService, ILogger<MessageSupportHistoryApp> logger)
        {
            _supabaseService = supabaseService;
            _logger = logger;
            LogId = Guid.NewGuid().ToString();
        }

        public async Task<List<MessageSupportHistoryViewModel.Response>> GetMessageSupportHistoryAsync(MessageSupportHistoryViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("MessageSupportHistoryId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("MessageSupportHistoryId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<MessageSupportHistoryModel>()
                    .Select("*")
                    .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new MessageSupportHistoryViewModel.Response
                {
                    Id = model.Id,
                    Message = model.Message,
                    IsAdminResponse = model.IsAdminResponse,                 
                    SupportMessageId = model.SupportMessageId,
                    UserId = model.UserId,
                    CreatedAt = model.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessageSupportHistoryApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }
        
        public async Task<List<MessageSupportHistoryViewModel.Response>> GetMessageListSupportHistoryAsync(MessageSupportHistoryViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("MessageSupportHistoryId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("MessageSupportHistoryId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<MessageSupportHistoryModel>()
                    .Select("*")
                    .Filter("support_message_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new MessageSupportHistoryViewModel.Response
                {
                    Id = model.Id,
                    Message = model.Message,
                    IsAdminResponse = model.IsAdminResponse,                 
                    SupportMessageId = model.SupportMessageId,
                    UserId = model.UserId,
                    CreatedAt = model.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessageSupportHistoryApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<List<MessageSupportHistoryViewModel.Response>> GetMessageUniqueSupportHistoryAsync(MessageSupportHistoryViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("MessageSupportHistoryId não pode ser nulo ou vazio.");

                if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("MessageSupportHistoryId inválido. Deve ser um UUID.");

                var client = _supabaseService.GetClient();

                var result = await client
                    .From<MessageSupportHistoryModel>()
                    .Select("*")
                    .Filter("support_message_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new MessageSupportHistoryViewModel.Response
                {
                    Id = model.Id,
                    Message = model.Message,
                    IsAdminResponse = model.IsAdminResponse,
                    SupportMessageId = model.SupportMessageId,
                    UserId = model.UserId,
                    CreatedAt = model.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessageSupportHistoryApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<MessageSupportHistoryViewModel.Response> CreateMessageSupportHistoryAsync(MessageSupportHistoryViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new MessageSupportHistoryModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Message = request.Message,
                    IsAdminResponse = request.IsAdminResponse,
                    SupportMessageId = request.SupportMessageId,
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };

                var result = await client
                    .From<MessageSupportHistoryModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new MessageSupportHistoryViewModel.Response
                {
                    Id = createdModel.Id,
                    Message = createdModel.Message,
                    IsAdminResponse = createdModel.IsAdminResponse,
                    SupportMessageId = createdModel.SupportMessageId,
                    CreatedAt = createdModel.CreatedAt,
                    UserId = createdModel.UserId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessageSupportHistoryApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<MessageSupportHistoryViewModel.Response> UpdateMessageSupportHistoryAsync(MessageSupportHistoryViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new MessageSupportHistoryModel
                {
                    Id = request.Id,
                    Message = request.Message,
                    IsAdminResponse = request.IsAdminResponse,
                    SupportMessageId = request.SupportMessageId,
                    UserId = request.UserId
                };               

                var result = await client
                    .From<MessageSupportHistoryModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new MessageSupportHistoryViewModel.Response
                {
                    Id = updatedModel.Id,
                    Message = updatedModel.Message,
                    IsAdminResponse = updatedModel.IsAdminResponse,
                    SupportMessageId = updatedModel.SupportMessageId,
                    UserId = updatedModel.UserId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessageSupportHistoryApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteMessageSupportHistoryAsync(MessageSupportHistoryViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<MessageSupportHistoryModel>()
                    .Where(x => x.Id == request.Id)
                    .Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(MessageSupportHistoryApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao excluir condominio", ex);
            }
        }
    }    
}

