using ApiSindisure.Domain.ViewModel.UserPermissionsViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.UserPermissions
{
    public interface IUserPermissionsApp
    {
        Task<List<UserPermissionsViewModel.Response>> GetUserPermissionsAsync(UserPermissionsViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<UserPermissionsViewModel.Response> CreateUserPermissionsAsync(UserPermissionsViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<UserPermissionsViewModel.Response> UpdateUserPermissionsAsync(UserPermissionsViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteUserPermissionsAsync(UserPermissionsViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

