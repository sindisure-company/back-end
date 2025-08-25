using ApiSindisure.Domain.ViewModel.PaymentsAppViewModel;

namespace ApiSindisure.Domain.Interfaces.Apps.Payments
{
    public interface IPaymentsApp
    {
        Task<string> PayWithCreditCardAsync(PaymentsAppViewModel.CreateRequest request, CancellationToken cancellationToken);
        Task<string> PayWithPixAsync(PaymentsAppViewModel.CreateRequest request, CancellationToken cancellationToken);
    }
}

