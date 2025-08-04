using ApiSindisure.Domain.ViewModel.AccountsReceivable;

namespace ApiSindisure.Domain.Interfaces.Apps.AccountsReceivable
{
    public interface IAccountsReceivableApp
    {
        Task<List<AccountsReceivableViewModel.Response>> GetAccountsReceivableAsync(AccountsReceivableViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<AccountsReceivableViewModel.Response> CreateAccountsReceivableAsync(AccountsReceivableViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<AccountsReceivableViewModel.Response> UpdateAccountsReceivableAsync(AccountsReceivableViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task<List<AccountsReceivableViewModel.Response>> UpdateAccountsReceivablePendingFeesAsync(AccountsReceivableViewModel.UpdateManyRequest request, CancellationToken cancellationToken);
        Task DeleteAccountsReceivableAsync(AccountsReceivableViewModel.DeleteRequest request, CancellationToken cancellationToken);
        Task DeleteAccountsReceivablePendingAsync(AccountsReceivableViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

