using ApiSindisure.Domain.ViewModel.Audit;

namespace ApiSindisure.Domain.Interfaces.Apps.Audit
{
    public interface IAuditApp
    {
        Task<AuditViewModel.Response> LogAuditAsync(AuditViewModel.LogRequest request, CancellationToken cancellationToken);
        Task<List<AuditViewModel.Response>> GetAuditLogsAsync(AuditViewModel.GetRequest request, CancellationToken cancellationToken);
    }
}

