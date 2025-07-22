using ApiSindisure.Domain.ViewModel.Login;

namespace ApiSindisure.Domain.Interfaces.Apps.Login
{
    public interface ILoginApp
    {
        Task<LoginViewModel.Response> HandleAsync(LoginViewModel.Request request, CancellationToken cancellationToken);
    }
}
