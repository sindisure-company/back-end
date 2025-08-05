using ApiSindisure.Domain.ViewModel.UserDetailsViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.UserDetails
{
    public interface IUserDetailsApp
    {
        Task<List<UserDetailsViewModel.Response>> GetUserDetailsAsync(UserDetailsViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<UserDetailsViewModel.Response> CreateUserDetailsAsync(UserDetailsViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<UserDetailsViewModel.Response> UpdateUserDetailsAsync(UserDetailsViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteUserDetailsAsync(UserDetailsViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

