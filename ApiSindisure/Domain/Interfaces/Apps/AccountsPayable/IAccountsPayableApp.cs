using ApiSindisure.Domain.ViewModel.AccountsPayable;

namespace ApiSindisure.Domain.Interfaces.Apps.AccountsPayable
{
    public interface IAccountsPayableApp
    {
        Task<List<AccountsPayableViewModel.Response>> GetAccountsPayableAsync(AccountsPayableViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<List<AccountsPayableViewModel.Response>> GetUpcommingAccountsPayableAsync(AccountsPayableViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<List<AccountsPayableViewModel.Response>> GetAccountsPayablePendingRecurringAsync(AccountsPayableViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<AccountsPayableViewModel.Response> CreateAccountsPayableAsync(AccountsPayableViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<List<AccountsPayableViewModel.Response>> CreateRecurringAccountsPayableAsync(List<AccountsPayableViewModel.CreateRequest> request, CancellationToken cancellationToken);
        Task<AccountsPayableViewModel.Response> UpdateAccountsPayableAsync(AccountsPayableViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task<AccountsPayableViewModel.Response> UpdateAccountsPayableStatusAsync(AccountsPayableViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteAccountsPayableAsync(AccountsPayableViewModel.DeleteRequest request, CancellationToken cancellationToken);
        Task DeleteUpcommingAccountsPayableAsync(AccountsPayableViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

