using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.AccountsReceivable
{
    public class AccountsReceivableViewModel
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

            [Required]
            [JsonPropertyName("payer")]
            public string Payer { get; set; }

            [JsonPropertyName("notes")]
            public string? Notes { get; set; }

            [JsonPropertyName("created_by")]
            public string CreateBy { get; set; }

            [Required]
            [JsonPropertyName("condominium_id")]
            public string CondominiumId { get; set; }
            
            [JsonPropertyName("file_name")]
            public string? FileName { get; set; }
            
            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]
            public string Description { get; set; }

            [Required]
            public decimal Amount { get; set; }

            [Required]
            public string Status { get; set; }

            public string? Company { get; set; }            

            [Required]
            public string Category { get; set; }

            [Required]
            [JsonPropertyName("payer")]
            public string Payer { get; set; }

            public string? Notes { get; set; }

            [JsonPropertyName("invoice_number")]
            public string? InvoiceNumber { get; set; }
            [Required]
            public DateTime DueDate { get; set; }
            public string? FileName { get; set; }
            public string? FileUrl { get; set; }
            [Required]
            [JsonPropertyName("created_by")]
            public string CreateBy { get; set; }
            [Required]
            [JsonPropertyName("condominium_id")]
            public string CondominiumId { get; set; }
        }

        public class DeleteRequest : BaseEntity
        {}
        
        public class UpdateManyRequest 
        {
            [JsonPropertyName("updates")]
            public List<UpdateItem> Updates { get; set; }
            public string? CondominiumId { get; set; }

            public class UpdateItem 
            {
                [JsonPropertyName("id")]
                public string? Id { get; set; }

                [JsonPropertyName("amount")]
                public decimal Amount { get; set; }

                [JsonPropertyName("due_day")]
                public int DueDay { get; set; }

                [JsonPropertyName("notes")]
                public string Notes { get; set; }
            }
        }

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

            [Required]
            [JsonPropertyName("payer")]
            public string Payer { get; set; }

            [JsonPropertyName("notes")]
            public string? Notes { get; set; }

            [JsonPropertyName("created_by")]
            public string CreateBy { get; set; }

            [Required]
            [JsonPropertyName("condominium_id")]
            public string CondominiumId { get; set; }

            [JsonPropertyName("file_name")]
            public string? FileName { get; set; }

            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }
}

