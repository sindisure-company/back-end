using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.EmailAutomationViewModel
{
    public class EmailAutomationViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {
            [Required]
            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [Required]
            [JsonPropertyName("is_active")]
            public bool IsActive  { get; set; }

            [JsonPropertyName("responsible_name")]
            public string? ResponsibleName { get; set; }

            [Required]
            [JsonPropertyName("send_cash_flow")]
            public bool SendCashFlow { get; set; }
       
            [JsonPropertyName("send_files")]
            public bool SendFiles { get; set; }
            
            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]
            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [Required]
            [JsonPropertyName("is_active")]
            public bool IsActive  { get; set; }

            [JsonPropertyName("responsible_name")]
            public string? ResponsibleName { get; set; }

            [Required]
            [JsonPropertyName("send_cash_flow")]
            public bool SendCashFlow { get; set; }
           
            [JsonPropertyName("send_files")]
            public bool SendFiles { get; set; }
            
            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
           
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [Required]
            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [Required]
            [JsonPropertyName("is_active")]
            public bool IsActive  { get; set; }

            [JsonPropertyName("responsible_name")]
            public string? ResponsibleName { get; set; }

            [Required]
            [JsonPropertyName("send_cash_flow")]
            public bool SendCashFlow { get; set; }
     
            [JsonPropertyName("send_files")]
            public bool SendFiles { get; set; }
            
            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
            [Required]
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [Required]
            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }
            
        }
    }
}

