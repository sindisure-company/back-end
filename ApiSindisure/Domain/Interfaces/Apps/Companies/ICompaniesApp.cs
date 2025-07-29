using ApiSindisure.Domain.ViewModel.CompaniesViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.Companies
{
    public interface ICompaniesApp
    {
        Task<List<CompaniesViewModel.Response>> GetCompaniesAsync(CompaniesViewModel.GetRequest request, CancellationToken cancellationToken);
        Task<CompaniesViewModel.Response> CreateCompaniesAsync(CompaniesViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<CompaniesViewModel.Response> UpdateCompaniesAsync(CompaniesViewModel.UpdateRequest request, CancellationToken cancellationToken);
        Task DeleteCompaniesAsync(CompaniesViewModel.DeleteRequest request, CancellationToken cancellationToken);
    }
}

