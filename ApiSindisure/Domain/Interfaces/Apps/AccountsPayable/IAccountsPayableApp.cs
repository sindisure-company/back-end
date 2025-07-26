using ApiSindisure.Domain.ViewModel.AccountsPayable;

namespace ApiSindisure.Domain.Interfaces.Apps.AccountsPayable
{
    public interface IAccountsPayableApp
    {
        Task<List<AccountsPayableViewModel.Response>> GetAccountsPayableAsync(AccountsPayableViewModel.GetRequest request, CancellationToken cancellationToken);
        // Task<AccountsPayableViewModel.Response> CreateAccountsPayableAsync(AccountsPayableViewModel.CreateRequest request, CancellationToken cancellationToken);
        // Task<AccountsPayableViewModel.Response> UpdateAccountsPayableAsync(AccountsPayableViewModel.UpdateRequest request, CancellationToken cancellationToken);
        // Task DeleteAccountsPayableAsync(AccountsPayableViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

