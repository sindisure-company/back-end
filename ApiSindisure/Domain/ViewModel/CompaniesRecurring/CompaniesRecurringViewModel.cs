using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.CompaniesRecurringViewModel
{
    public class CompaniesRecurringViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {   
            [Required]        
            [JsonPropertyName("amount")]
            public decimal Amount { get; set; }
            
            [Required]        
            [JsonPropertyName("company_id")]
            public string? CompanyId { get; set; }

            [Required]
            [JsonPropertyName("category")]
            public string? Category { get; set; }

            [Required]
            [JsonPropertyName("condominium_id")]
            public string? CondominiumId { get; set; }

            [Required]
            [JsonPropertyName("description")]
            public string? Description { get; set; }

            [Required]
            [JsonPropertyName("due_day")]
            public int DueDay { get; set; }

            [Required]
            [JsonPropertyName("is_active")]
            public bool IsActive { get; set; }

            [JsonPropertyName("notes")]
            public string? Notes { get; set; }

            [Required]
            [JsonPropertyName("recurrence_type")]
            public string RecurrenceType { get; set; }
            
            [Required]
            [JsonPropertyName("created_by")]
            public string CreatedBy { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]        
            [JsonPropertyName("amount")]
            public decimal Amount { get; set; }

            [Required]        
            [JsonPropertyName("company_id")]
            public string? CompanyId { get; set; }

            [Required]
            [JsonPropertyName("category")]
            public string? Category { get; set; }

            [Required]
            [JsonPropertyName("condominium_id")]
            public string? CondominiumId { get; set; }

            [Required]
            [JsonPropertyName("description")]
            public string? Description { get; set; }

            [Required]
            [JsonPropertyName("due_day")]
            public int DueDay { get; set; }

            [Required]
            [JsonPropertyName("is_active")]
            public bool IsActive { get; set; }

            [JsonPropertyName("notes")]
            public string? Notes { get; set; }

            [Required]
            [JsonPropertyName("recurrence_type")]
            public string RecurrenceType { get; set; }

            [JsonPropertyName("created_by")]
            public string CreatedBy { get; set; }
           
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [Required]        
            [JsonPropertyName("amount")]
            public decimal Amount { get; set; }

            [Required]        
            [JsonPropertyName("company_id")]
            public string? CompanyId { get; set; }

            [Required]
            [JsonPropertyName("category")]
            public string? Category { get; set; }

            [Required]
            [JsonPropertyName("condominium_id")]
            public string? CondominiumId { get; set; }

            [Required]
            [JsonPropertyName("description")]
            public string? Description { get; set; }

            [Required]
            [JsonPropertyName("due_day")]
            public int DueDay { get; set; }

            [Required]
            [JsonPropertyName("is_active")]
            public bool IsActive { get; set; }

            [JsonPropertyName("notes")]
            public string? Notes { get; set; }

            [Required]
            [JsonPropertyName("recurrence_type")]
            public string RecurrenceType { get; set; }

            [JsonPropertyName("created_by")]
            public string CreatedBy { get; set; }

            [Required]
            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }  

            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }  
        }
    }
}

