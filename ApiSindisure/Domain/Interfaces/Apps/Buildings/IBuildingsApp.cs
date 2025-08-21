using ApiSindisure.Domain.ViewModel.Buildings;

namespace ApiSindisure.Domain.Interfaces.Apps.Buildings
{
    public interface IBuildingsApp
    {
        Task<List<BuildingsViewModel.Response>> GetBuildingsAsync(BuildingsViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<BuildingsViewModel.Response> CreateBuildingsAsync(BuildingsViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<BuildingsViewModel.Response> UpdateBuildingsAsync(BuildingsViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteBuildingsAsync(BuildingsViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

