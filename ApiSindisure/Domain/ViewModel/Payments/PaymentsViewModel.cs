using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.PaymentsAppViewModel
{
    public class PaymentsAppViewModel
    {
        public class GetRequest : BaseEntity
        { }

        public class CreateRequest
        {
            [Required]
            [JsonPropertyName("name")]
            public string? Name { get; set; }

            [Required]
            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [Required]
            [JsonPropertyName("cpf")]
            public string? Cpf { get; set; }

            [Required]
            [JsonPropertyName("amount")]
            public decimal Amount { get; set; }

            [Required]
            [JsonPropertyName("card_hash")]
            public string? CardHash { get; set; }

            [Required]
            [JsonPropertyName("customer_id")]
            public string? CustomerId { get; set; }

        }

        public class Response
        {
            [Required]
            [JsonPropertyName("sucess")]
            public bool Sucess { get; set; }

            [Required]
            [JsonPropertyName("qr_code")]
            public string? QrCode { get; set; }

            [Required]
            [JsonPropertyName("error_message")]
            public string? ErrorMessage { get; set; }
        }
    }
}

