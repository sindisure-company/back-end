using ApiSindisure.Domain.ViewModel.MessageSupportHistoryViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.MessageSupportHistory
{
    public interface IMessageSupportHistoryApp
    {
        Task<List<MessageSupportHistoryViewModel.Response>> GetMessageSupportHistoryAsync(MessageSupportHistoryViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<List<MessageSupportHistoryViewModel.Response>> GetMessageUniqueSupportHistoryAsync(MessageSupportHistoryViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<List<MessageSupportHistoryViewModel.Response>> GetMessageListSupportHistoryAsync(MessageSupportHistoryViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<MessageSupportHistoryViewModel.Response> CreateMessageSupportHistoryAsync(MessageSupportHistoryViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<MessageSupportHistoryViewModel.Response> UpdateMessageSupportHistoryAsync(MessageSupportHistoryViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteMessageSupportHistoryAsync(MessageSupportHistoryViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

