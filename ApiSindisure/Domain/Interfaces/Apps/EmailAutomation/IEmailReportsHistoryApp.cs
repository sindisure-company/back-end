using ApiSindisure.Domain.ViewModel.EmailReportsHistoryViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.EmailReportsHistory
{
    public interface IEmailReportsHistoryApp
    {
        Task<List<EmailReportsHistoryViewModel.Response>> GetEmailReportsHistoryAsync(EmailReportsHistoryViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<EmailReportsHistoryViewModel.Response> CreateEmailReportsHistoryAsync(EmailReportsHistoryViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<EmailReportsHistoryViewModel.Response> UpdateEmailReportsHistoryAsync(EmailReportsHistoryViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteEmailReportsHistoryAsync(EmailReportsHistoryViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

