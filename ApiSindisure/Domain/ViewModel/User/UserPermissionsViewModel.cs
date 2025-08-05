using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.UserPermissionsViewModel
{
    public class UserPermissionsViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {   
            [Required]        
            [JsonPropertyName("amount")]
            public string? Roles { get; set; }

            [Required]
            [JsonPropertyName("category")]
            public string? UpdatedAt { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]        
            [JsonPropertyName("amount")]
            public string? Roles { get; set; }

            [Required]
            [JsonPropertyName("updated_at")]
            public string? UpdatedAt { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
           
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [Required]        
            [JsonPropertyName("amount")]
            public string? Roles { get; set; }

            [Required]
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [Required]
            [JsonPropertyName("category")]
            public DateTime CreatedAt { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
        }
    }
}

