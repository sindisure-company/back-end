using ApiSindisure.Domain.ViewModel.NotificationHistoryViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.NotificationHistory
{
    public interface INotificationHistoryApp
    {
        Task<List<NotificationHistoryViewModel.Response>> GetNotificationHistoryAsync(NotificationHistoryViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<NotificationHistoryViewModel.Response> CreateNotificationHistoryAsync(NotificationHistoryViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<NotificationHistoryViewModel.Response> UpdateNotificationHistoryAsync(NotificationHistoryViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteNotificationHistoryAsync(NotificationHistoryViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

