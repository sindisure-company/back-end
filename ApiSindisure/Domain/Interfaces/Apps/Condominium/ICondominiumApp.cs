using ApiSindisure.Domain.ViewModel.Condominium;

namespace ApiSindisure.Domain.Interfaces.Apps.Condominium
{
    public interface ICondominiumApp
    {
        Task<List<CondominiumViewModel.Response>> GetCondominiumAsync(CondominiumViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<CondominiumViewModel.Response> CreateCondominiumAsync(CondominiumViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<CondominiumViewModel.Response> UpdateCondominiumAsync(CondominiumViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteCondominiumAsync(CondominiumViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

