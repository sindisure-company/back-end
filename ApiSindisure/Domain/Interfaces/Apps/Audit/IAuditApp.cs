using ApiSindisure.Domain.ViewModel.Audit;

namespace ApiSindisure.Domain.Interfaces.Apps.Audit
{
    public interface IAuditApp
    {
        Task LogAuditAsync(AuditViewModel.LogRequest request, CancellationToken cancellationToken);
        Task<List<AuditViewModel.Response>> GetAuditLogsAsync(AuditViewModel.GetRequest request, CancellationToken cancellationToken);
    }
}

