using ApiSindisure.Domain.ViewModel.UserPlansHistoryViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.UserPlansHistory
{
    public interface IUserPlansHistoryApp
    {
        Task<List<UserPlansHistoryViewModel.Response>> GetUserPlansHistoryAsync(UserPlansHistoryViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<UserPlansHistoryViewModel.Response> CreateUserPlansHistoryAsync(UserPlansHistoryViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<UserPlansHistoryViewModel.Response> UpdateUserPlansHistoryAsync(UserPlansHistoryViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteUserPlansHistoryAsync(UserPlansHistoryViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

