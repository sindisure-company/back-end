using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.UserProfilesViewModel
{
    public class UserProfilesViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {
            [Required]
            [JsonPropertyName("address")]
            public string? Address { get; set; }

            [Required]
            [JsonPropertyName("avatar_url")]
            public string? AvatarUrl { get; set; }

            [Required]
            [JsonPropertyName("city")]
            public string? City { get; set; }

            [Required]
            [JsonPropertyName("date_of_birth")]
            public string? DateOfBirth { get; set; }

            [Required]
            [JsonPropertyName("document_number")]
            public string? DocumentNumber { get; set; }

            [Required]
            [JsonPropertyName("first_name")]
            public string? FirstName { get; set; }

            [Required]
            [JsonPropertyName("last_name")]
            public string? LastName { get; set; }

            [Required]
            [JsonPropertyName("neighborhood")]
            public string? Neighborhood { get; set; }

            [Required]
            [JsonPropertyName("number")]
            public string? Number { get; set; }

            [Required]
            [JsonPropertyName("phone")]
            public string? Phone { get; set; }

            [Required]
            [JsonPropertyName("state")]
            public string? State { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]
            [JsonPropertyName("address")]
            public string? Address { get; set; }

            [Required]
            [JsonPropertyName("avatar_url")]
            public string? AvatarUrl { get; set; }

            [Required]
            [JsonPropertyName("city")]
            public string? City { get; set; }

            [Required]
            [JsonPropertyName("date_of_birth")]
            public string? DateOfBirth { get; set; }

            [Required]
            [JsonPropertyName("document_number")]
            public string? DocumentNumber { get; set; }

            [Required]
            [JsonPropertyName("first_name")]
            public string? FirstName { get; set; }

            [Required]
            [JsonPropertyName("last_name")]
            public string? LastName { get; set; }

            [Required]
            [JsonPropertyName("neighborhood")]
            public string? Neighborhood { get; set; }

            [Required]
            [JsonPropertyName("number")]
            public string? Number { get; set; }

            [Required]
            [JsonPropertyName("phone")]
            public string? Phone { get; set; }

            [Required]
            [JsonPropertyName("state")]
            public string? State { get; set; }

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
            [JsonPropertyName("address")]
            public string? Address { get; set; }

            [Required]
            [JsonPropertyName("avatar_url")]
            public string? AvatarUrl { get; set; }

            [Required]
            [JsonPropertyName("city")]
            public string? City { get; set; }

            [Required]
            [JsonPropertyName("date_of_birth")]
            public string? DateOfBirth { get; set; }

            [Required]
            [JsonPropertyName("document_number")]
            public string? DocumentNumber { get; set; }

            [Required]
            [JsonPropertyName("first_name")]
            public string? FirstName { get; set; }

            [Required]
            [JsonPropertyName("last_name")]
            public string? LastName { get; set; }

            [Required]
            [JsonPropertyName("neighborhood")]
            public string? Neighborhood { get; set; }

            [Required]
            [JsonPropertyName("number")]
            public string? Number { get; set; }

            [Required]
            [JsonPropertyName("phone")]
            public string? Phone { get; set; }

            [Required]
            [JsonPropertyName("state")]
            public string? State { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }

            [Required]        
            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }

            [Required]
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }
         
        }
    }
}

