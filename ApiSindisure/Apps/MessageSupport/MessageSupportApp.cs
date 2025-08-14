using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.MessageSupport;
using ApiSindisure.Domain.ViewModel.MessageSupportViewModel;
using ApiSindisure.Services.Supabase;

using Supabase.Postgrest.Attributes;

namespace ApiSindisure.Apps.MessageSupport
{
    public class MessageSupportApp : IMessageSupportApp
    {
        private readonly SupabaseService _supabaseService;

        public MessageSupportApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<MessageSupportViewModel.Response>> GetMessageSupportAsync(MessageSupportViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("MessageSupportId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("MessageSupportId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<MessageSupportModel>()
                    .Select("*")
                    .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new MessageSupportViewModel.Response
                {
                    Id = model.Id,
                    Message = model.Message,
                    Read = model.Read,                 
                    Subject = model.Subject,
                    UserId = model.UserId,
                    UpdatedAt = model.UpdatedAt,
                    CreatedAt = model.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<MessageSupportViewModel.Response> CreateMessageSupportAsync(MessageSupportViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var client = _supabaseService.GetClient();
                var model = new MessageSupportModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Message = request.Message,
                    Read = request.Read,                 
                    Subject = request.Subject,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };

                var result = await client
                    .From<MessageSupportModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new MessageSupportViewModel.Response
                {
                    Id = createdModel.Id,
                    Message = createdModel.Message,
                    Read = createdModel.Read,
                    Subject = createdModel.Subject,
                    CreatedAt = createdModel.CreatedAt,
                    UpdatedAt = createdModel.UpdatedAt,
                    UserId = createdModel.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<MessageSupportViewModel.Response> UpdateMessageSupportAsync(MessageSupportViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new MessageSupportModel
                {
                    Id = request.Id,
                    Message = request.Message,
                    Read = request.Read,
                    Subject = request.Subject,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };               

                var result = await client
                    .From<MessageSupportModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new MessageSupportViewModel.Response
                {
                    Id = updatedModel.Id,
                    Message = updatedModel.Message,
                    Read = updatedModel.Read,
                    Subject = updatedModel.Subject,
                    UpdatedAt = updatedModel.UpdatedAt,
                    UserId = updatedModel.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteMessageSupportAsync(MessageSupportViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<MessageSupportModel>()
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

