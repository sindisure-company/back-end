using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.MessageSupportHistoryViewModel
{
    public class MessageSupportHistoryViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {
            [Required]
            [JsonPropertyName("message")]
            public string? Message { get; set; }

            [Required]
            [JsonPropertyName("is_admin_response")]
            public bool IsAdminResponse { get; set; }

            [Required]
            [JsonPropertyName("support_message_id")]
            public string? SupportMessageId { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]
            [JsonPropertyName("message")]
            public string? Message { get; set; }

            [Required]
            [JsonPropertyName("is_admin_response")]
            public bool IsAdminResponse { get; set; }

            [Required]
            [JsonPropertyName("support_message_id")]
            public string? SupportMessageId { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }

            [Required]
            [JsonPropertyName("update_at")]
            public string? UpdatedAt { get; set; }
           
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [Required]
            [JsonPropertyName("message")]
            public string? Message { get; set; }

            [Required]
            [JsonPropertyName("is_admin_response")]
            public bool IsAdminResponse { get; set; }

            [Required]
            [JsonPropertyName("support_message_id")]
            public string? SupportMessageId { get; set; }

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

