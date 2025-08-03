using ApiSindisure.Domain.ViewModel.CompaniesRecurringViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.CompaniesRecurring
{
    public interface ICompaniesRecurringApp
    {
        Task<List<CompaniesRecurringViewModel.Response>> GetCompaniesRecurringAsync(CompaniesRecurringViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<CompaniesRecurringViewModel.Response> CreateCompaniesRecurringAsync(CompaniesRecurringViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<CompaniesRecurringViewModel.Response> UpdateCompaniesRecurringAsync(CompaniesRecurringViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteCompaniesRecurringAsync(CompaniesRecurringViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

