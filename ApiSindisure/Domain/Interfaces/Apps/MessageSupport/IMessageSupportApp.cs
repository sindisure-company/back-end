using ApiSindisure.Domain.ViewModel.MessageSupportViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.MessageSupport
{
    public interface IMessageSupportApp
    {
        Task<List<MessageSupportViewModel.Response>> GetMessageSupportAsync(MessageSupportViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<MessageSupportViewModel.Response> CreateMessageSupportAsync(MessageSupportViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<MessageSupportViewModel.Response> UpdateMessageSupportAsync(MessageSupportViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteMessageSupportAsync(MessageSupportViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

