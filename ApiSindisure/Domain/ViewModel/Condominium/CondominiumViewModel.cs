using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.Condominium
{
    public class CondominiumViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {
            [Required]
            [JsonPropertyName("address")]
            public string Address { get; set; }

            [Required]
            [JsonPropertyName("email")]
            public string Email { get; set; }

            [Required]
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("observation")]
            public string Observation { get; set; }

            [JsonPropertyName("personal_contact")]
            public string? PersonalContact { get; set; }

            [Required]
            [JsonPropertyName("phone")]
            public string Phone { get; set; }

            [JsonPropertyName("units")]
            public int Units { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("created_by")]
            public string CreatedBy { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]
            [JsonPropertyName("address")]
            public string Address { get; set; }

            [Required]
            [JsonPropertyName("email")]
            public string Email { get; set; }

            [Required]
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("observation")]
            public string Observation { get; set; }

            [JsonPropertyName("personal_contact")]
            public string? PersonalContact { get; set; }

            [Required]
            [JsonPropertyName("phone")]
            public string Phone { get; set; }

            [JsonPropertyName("units")]
            public int Units { get; set; }

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
            [Required]
            [JsonPropertyName("address")]
            public string Address { get; set; }

            [Required]
            [JsonPropertyName("email")]
            public string Email { get; set; }

            [Required]
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [Required]
            [JsonPropertyName("observation")]
            public string Observation { get; set; }

            [JsonPropertyName("personal_contact")]
            public string? PersonalContact { get; set; }

            [Required]
            [JsonPropertyName("phone")]
            public string Phone { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("units")]
            public int Units { get; set; }

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

