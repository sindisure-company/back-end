using ApiSindisure.Domain.ViewModel.UserPlansViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.UserPlans
{
    public interface IUserPlansApp
    {
        Task<List<UserPlansViewModel.Response>> GetUserPlansAsync(UserPlansViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<UserPlansViewModel.Response> CreateUserPlansAsync(UserPlansViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<UserPlansViewModel.Response> UpdateUserPlansAsync(UserPlansViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteUserPlansAsync(UserPlansViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

