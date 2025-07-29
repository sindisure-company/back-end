using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.Buildings
{
    public class BuildingsViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {
            [Required]
            [JsonPropertyName("condominium_id")]
            public string CondominiumId { get; set; }
     

            [JsonPropertyName("observation")]
            public string Observation { get; set; }

            [JsonPropertyName("personal_contact")]
            public string? PersonalContact { get; set; }

            [Required]
            [JsonPropertyName("phone")]
            public string Phone { get; set; }

            [Required]
            [JsonPropertyName("unit_number")]
            public int UnitNumber { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("created_by")]
            public string CreatedBy { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]
            [JsonPropertyName("condominium_id")]
            public string CondominiumId { get; set; }

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

            [JsonPropertyName("unit_number")]
            public int UnitNumber { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("created_by")]
            public string CreatedBy { get; set; }

            [Required]
            [JsonPropertyName("created_at")]
            public string CreatedAt { get; set; }  

            [JsonPropertyName("updated_at")]
            public string UpdateAt { get; set; }  
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [Required]
            [JsonPropertyName("condominium_id")]
            public string CondominiumId { get; set; }

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

            [JsonPropertyName("unit_number")]
            public int UnitNumber { get; set; }

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

