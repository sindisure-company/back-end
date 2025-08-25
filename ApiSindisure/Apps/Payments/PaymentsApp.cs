using ApiSindisure.Domain.Interfaces.Apps.Payments;
using ApiSindisure.Domain.ViewModel.PaymentsAppViewModel;
using Newtonsoft.Json;

namespace ApiSindisure.Apps.Login
{
    public class PaymentsApp : IPaymentsApp
    {
        private readonly PaymentService _paymentsService;
        private readonly ILogger<PaymentsApp> _logger;
        public string LogId { get; set; }

        public PaymentsApp(PaymentService paymentsService, ILogger<PaymentsApp> logger)
        {
            _paymentsService = paymentsService;
            _logger = logger;
            LogId = Guid.NewGuid().ToString();
        }

        public async Task<string> PayWithCreditCardAsync(PaymentsAppViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var paymentResponse = await _paymentsService.PayWithCreditCard(request);
                return paymentResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PaymentsApp)} - Erro ao processar o pagamento com cartão de crédito.: {LogId}" + ex.Message, ex);
                return ex.Message;
            }
        }

        public async Task<string> PayWithPixAsync(PaymentsAppViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var paymentResponse = await _paymentsService.PayWithPix(request);
                return paymentResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PaymentsApp)} - Erro ao processar o pagamento com pix: {LogId}" + ex.Message, ex);
                return ex.Message;
            }
        }
    }
}
