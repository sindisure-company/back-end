using ApiSindisure.Domain.ViewModel.EmailAutomationViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.EmailAutomation
{
    public interface IEmailAutomationApp
    {
        Task<List<EmailAutomationViewModel.Response>> GetEmailAutomationAsync(EmailAutomationViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<EmailAutomationViewModel.Response> CreateEmailAutomationAsync(EmailAutomationViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<EmailAutomationViewModel.Response> UpdateEmailAutomationAsync(EmailAutomationViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteEmailAutomationAsync(EmailAutomationViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

