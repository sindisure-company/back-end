using ApiSindisure.Domain.ViewModel.Login;
using ApiSindisure.Domain.ViewModel.UserRegisterViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.Login
{
    public interface ILoginApp
    {
        Task<LoginViewModel.Response> HandleAsync(LoginViewModel.Request request, CancellationToken cancellationToken);
        Task<UserRegisterViewModel.Response> SignUp(UserRegisterViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<LoginViewModel.Response> ResetPasswordAsync(LoginViewModel.ResetPassword request, CancellationToken cancellationToken);
    }
}
