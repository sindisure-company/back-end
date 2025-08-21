using ApiSindisure.Domain.ViewModel.EmailViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.EmailReportsApp
{
    public interface IEmailReportsApp
    {
        Task SendMonthlyReportAsync(
             EmailViewModel.Request request,
             CancellationToken cancellationToken);       
    }
}

