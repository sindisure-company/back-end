using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.UserPlansViewModel
{
    public class UserPlansViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {

            [Required]
            [JsonPropertyName("payment_date")]
            public string? PaymentDate { get; set; }

            [Required]
            [JsonPropertyName("plan_name")]
            public string? PlanName { get; set; }

            [Required]
            [JsonPropertyName("plan_value")]
            public decimal PlanValue { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string? Status { get; set; }
            
            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {

            [Required]
            [JsonPropertyName("payment_date")]
            public string? PaymentDate { get; set; }

            [Required]
            [JsonPropertyName("plan_name")]
            public string? PlanName { get; set; }

            [Required]
            [JsonPropertyName("plan_value")]
            public decimal PlanValue { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string? Status { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }

            [Required]
            [JsonPropertyName("updated_at")]
            public string? UpdatedAt { get; set; }
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {

            [Required]
            [JsonPropertyName("payment_date")]
            public string? PaymentDate { get; set; }

            [Required]
            [JsonPropertyName("plan_name")]
            public string? PlanName { get; set; }

            [Required]
            [JsonPropertyName("plan_value")]
            public decimal PlanValue { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string? Status { get; set; }

            [Required]
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [Required]        
            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
        }
    }
}

