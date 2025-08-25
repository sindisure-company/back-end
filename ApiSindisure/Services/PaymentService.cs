using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ApiSindisure.Domain.ViewModel.PaymentsAppViewModel;

public class PaymentService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly ILogger<PaymentService> _logger;
    public string LogId { get; set; }

    public PaymentService(IConfiguration configuration, ILogger<PaymentService> logger)
    {
        _apiKey = configuration["PagarMe:ApiKeyTest"];
        _baseUrl = configuration["PagarMe:Url"];
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);
        _logger = logger;
        LogId = Guid.NewGuid().ToString();
    }

    public async Task<string> PayWithCreditCard(PaymentsAppViewModel.CreateRequest request)
    {
        try
        {
            var customerResponse = await CreateCustomerAsync(request);

            if (string.IsNullOrEmpty(customerResponse))
                throw new Exception("Erro ao criar cliente.");

            var payload = new
            {
                amount = (int)(request.Amount * 100),
                payment_method = "credit_card",
                card_hash = request.CardHash,
                customer = new { id = customerResponse }
            };

            var response = await _httpClient.PostAsync(
                _baseUrl + "transactions",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(PaymentService)} - Erro ao processar o pagamento com cartão de crédito.: {LogId}" + ex.Message, ex);
            throw new Exception("Erro ao processar o pagamento com cartão de crédito.", ex);
        }

    }

    public async Task<string> PayWithPix(PaymentsAppViewModel.CreateRequest request)
    {
        try
        {
            var customerResponse = await CreateCustomerAsync(request);

            if (string.IsNullOrEmpty(customerResponse))
                throw new Exception("Erro ao criar cliente.");

            var payload = new
            {
                amount = (int)(request.Amount * 100),
                payment_method = "pix",
                customer = new { id = customerResponse }
            };

            var response = await _httpClient.PostAsync(
                _baseUrl + "transactions",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(PaymentService)} - Erro ao processar o pagamento via Pix.: {LogId}" + ex.Message, ex);
            throw new Exception("Erro ao processar o pagamento via Pix.", ex);
        }

    }

    private async Task<string> CreateCustomerAsync(PaymentsAppViewModel.CreateRequest request)
    {
        try
        {
            var payload = new
            {
                name = request.Name,
                email = request.Email,
                document = request.Cpf,
                type = "individual"
            };

            var response = await _httpClient.PostAsync(
                _baseUrl + "customers",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(PaymentService)} - Erro ao criar cliente.: {LogId}" + ex.Message, ex);
            throw new Exception("Erro ao criar cliente.", ex);
        }
    }
}
