using ApiSindisure.Domain.ViewModel.UserProfilesViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.UserProfiles
{
    public interface IUserProfilesApp
    {
        Task<List<UserProfilesViewModel.Response>> GetUserProfilesAsync(UserProfilesViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<UserProfilesViewModel.Response> CreateUserProfilesAsync(UserProfilesViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<UserProfilesViewModel.Response> UpdateUserProfilesAsync(UserProfilesViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteUserProfilesAsync(UserProfilesViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

