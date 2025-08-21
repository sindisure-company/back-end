using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.UserDetailsViewModel
{
    public class UserDetailsViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {   
            [Required]
            [JsonPropertyName("auth_user_id")]
            public string AuthUserId { get; set; }

            [Required]
            [JsonPropertyName("email")]
            public string Email { get; set; }

            [Required]
            [JsonPropertyName("address")]
            public string? Address { get; set; }
       
            [JsonPropertyName("avatar_url")]
            public string? AvatarUrl { get; set; }

            [Required]
            [JsonPropertyName("city")]
            public string? City { get; set; }

            [Required]
            [JsonPropertyName("document_number")]
            public string? DocumentNumber { get; set; }

            [JsonPropertyName("file_name")]
            public string? FileName { get; set; }

            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }

            [Required]
            [JsonPropertyName("first_name")]
            public string? FirstName { get; set; }

            [JsonPropertyName("img_avatar")]
            public string? ImgAvatar { get; set; }

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
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]
            [JsonPropertyName("auth_user_id")]
            public string AuthUserId { get; set; }

            [Required]
            [JsonPropertyName("email")]
            public string Email { get; set; }

            [Required]
            [JsonPropertyName("address")]
            public string? Address { get; set; }
       
            [JsonPropertyName("avatar_url")]
            public string? AvatarUrl { get; set; }

            [Required]
            [JsonPropertyName("city")]
            public string? City { get; set; }

            [Required]
            [JsonPropertyName("document_number")]
            public string? DocumentNumber { get; set; }

            [JsonPropertyName("file_name")]
            public string? FileName { get; set; }

            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }

            [Required]
            [JsonPropertyName("first_name")]
            public string? FirstName { get; set; }

            [JsonPropertyName("img_avatar")]
            public string? ImgAvatar { get; set; }

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
           
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [Required]
            [JsonPropertyName("auth_user_id")]
            public string AuthUserId { get; set; }

            [Required]
            [JsonPropertyName("email")]
            public string Email { get; set; }

            [Required]
            [JsonPropertyName("address")]
            public string? Address { get; set; }
       
            [JsonPropertyName("avatar_url")]
            public string? AvatarUrl { get; set; }

            [Required]
            [JsonPropertyName("city")]
            public string? City { get; set; }

            [Required]
            [JsonPropertyName("document_number")]
            public string? DocumentNumber { get; set; }

            [JsonPropertyName("file_name")]
            public string? FileName { get; set; }

            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }

            [Required]
            [JsonPropertyName("first_name")]
            public string? FirstName { get; set; }

            [JsonPropertyName("img_avatar")]
            public string? ImgAvatar { get; set; }

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
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [Required]
            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }
            
        }
    }
}

