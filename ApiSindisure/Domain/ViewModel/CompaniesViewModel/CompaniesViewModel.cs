using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.CompaniesViewModel
{
    public class CompaniesViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {           
            [JsonPropertyName("address")]
            public string? Address { get; set; }

            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [Required]
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("observation")]
            public string? Observation { get; set; }

            [JsonPropertyName("personal_contact")]
            public string? PersonalContact { get; set; }
            
            [JsonPropertyName("phone")]
            public string? Phone { get; set; }

            [JsonPropertyName("document")]
            public string? Document { get; set; }

            [JsonPropertyName("category")]
            public string Category{ get; set; }

            [JsonPropertyName("notes")]
            public string? Notes{ get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("created_by")]
            public string CreatedBy { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {
            [JsonPropertyName("address")]
            public string? Address { get; set; }

            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [Required]
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("personal_contact")]
            public string? PersonalContact { get; set; }
            
            [JsonPropertyName("phone")]
            public string? Phone { get; set; }

            [JsonPropertyName("document")]
            public string? Document { get; set; }

            [JsonPropertyName("category")]
            public string Category{ get; set; }

            [JsonPropertyName("notes")]
            public string? Notes{ get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("created_by")]
            public string CreatedBy { get; set; }
           
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [JsonPropertyName("address")]
            public string Address { get; set; }

            [JsonPropertyName("email")]
            public string Email { get; set; }

            [Required]
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("observation")]
            public string Observation { get; set; }

            [JsonPropertyName("personal_contact")]
            public string? PersonalContact { get; set; }
            
            [JsonPropertyName("phone")]
            public string Phone { get; set; }

            [JsonPropertyName("document")]
            public string? Document { get; set; }

            [JsonPropertyName("category")]
            public string Category{ get; set; }

            [JsonPropertyName("notes")]
            public string Notes{ get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string Status { get; set; }

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

