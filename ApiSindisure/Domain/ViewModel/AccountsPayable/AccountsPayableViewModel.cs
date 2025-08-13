using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.AccountsPayable
{
    public class AccountsPayableViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {
            [Required]
            [JsonPropertyName("description")]
            public string Description { get; set; }

            [Required]
            [JsonPropertyName("amount")]
            public decimal Amount { get; set; }

            [Required]
            [JsonPropertyName("due_date")]
            public DateTime DueDate { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string Status { get; set; }
            
            [JsonPropertyName("company")]
            public string? Company { get; set; }

            [JsonPropertyName("invoice_number")]
            public string? InvoiceNumber { get; set; }

            [Required]
            [JsonPropertyName("category")]
            public string Category { get; set; }

            [JsonPropertyName("notes")]
            public string? Notes { get; set; }

            [JsonPropertyName("created_by")]
            public string CreateBy { get; set; }

            [Required]
            [JsonPropertyName("condominium_id")]
            public string CondominiumId { get; set; }
      
            [JsonPropertyName("companies_recurring_id")]
            public string? CompaniesRecurringId { get; set; }
            
            [JsonPropertyName("file_name")]
            public string? FileName { get; set; }
            
            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]
            [JsonPropertyName("description")]
            public string Description { get; set; }

            [Required]
            [JsonPropertyName("amount")]
            public decimal Amount { get; set; }

            [Required]
            public string Status { get; set; }

            [JsonPropertyName("company")]
            public string? Company { get; set; }

            [Required]
            [JsonPropertyName("category")]
            public string Category { get; set; }

            public string? Notes { get; set; }

            [JsonPropertyName("invoice_number")]
            public string? InvoiceNumber { get; set; }
            [Required]

            [JsonPropertyName("due_date")]
            public DateTime DueDate { get; set; }

            [JsonPropertyName("file_name")]
            public string? FileName { get; set; }

            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }
            [Required]
            [JsonPropertyName("created_by")]
            public string CreateBy { get; set; }

            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }
            
            [Required]
            [JsonPropertyName("condominium_id")]
            public string CondominiumId { get; set; }

            [JsonPropertyName("companies_recurring_id")]
            public string? CompaniesRecurringId { get; set; }
            
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [Required]
            [JsonPropertyName("description")]
            public string Description { get; set; }

            [Required]
            [JsonPropertyName("amount")]
            public decimal Amount { get; set; }

            [Required]
            [JsonPropertyName("due_date")]
            public DateTime DueDate { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string Status { get; set; }
            
            [JsonPropertyName("company")]
            public string? Company { get; set; }

            [JsonPropertyName("invoice_number")]
            public string? InvoiceNumber { get; set; }

            [Required]
            [JsonPropertyName("category")]
            public string Category { get; set; }

            [JsonPropertyName("notes")]
            public string? Notes { get; set; }

            [JsonPropertyName("created_by")]
            public string CreateBy { get; set; }

            [Required]
            [JsonPropertyName("condominium_id")]
            public string CondominiumId { get; set; }

            [JsonPropertyName("companies_recurring_id")]
            public string? CompaniesRecurringId { get; set; }
            
            [JsonPropertyName("file_name")]
            public string? FileName { get; set; }
            
            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }

            [JsonPropertyName("created_at")]
            public string? CreatedAt { get; set; }

            [JsonPropertyName("updated_by")]
            public string? UpdatedAt { get; set; }
        }
    }
}

