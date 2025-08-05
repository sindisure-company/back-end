using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.NotificationHistoryViewModel
{
    public class NotificationHistoryViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {
            [Required]
            [JsonPropertyName("notification_key")]
            public string? NotificationKey { get; set; }

            [Required]
            [JsonPropertyName("notification_type")]
            public string? NotificationType { get; set; }

            [JsonPropertyName("viewed_at")]
            public string? ViewedAt { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]
            [JsonPropertyName("notification_key")]
            public string? NotificationKey { get; set; }

            [Required]
            [JsonPropertyName("notification_type")]
            public string? NotificationType { get; set; }

            [JsonPropertyName("viewed_at")]
            public string? ViewedAt { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
           
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [Required]
            [JsonPropertyName("notification_key")]
            public string? NotificationKey { get; set; }

            [Required]
            [JsonPropertyName("notification_type")]
            public string? NotificationType { get; set; }

            [JsonPropertyName("viewed_at")]
            public string? ViewedAt { get; set; }

            [Required]
            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
        }
    }
}

