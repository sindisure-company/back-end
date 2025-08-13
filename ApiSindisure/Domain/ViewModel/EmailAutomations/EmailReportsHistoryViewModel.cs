using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.EmailReportsHistoryViewModel
{
    public class EmailReportsHistoryViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {
            [Required]
            [JsonPropertyName("automation_id")]
            public string? AutomationId { get; set; }

            [Required]
            [JsonPropertyName("file_name")]
            public string? FileName  { get; set; }

            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }

            [Required]
            [JsonPropertyName("recipient_email")]
            public string? RecipientEmail { get; set; }

            [Required]
            [JsonPropertyName("report_type")]
            public string? ReportType { get; set; }

            [Required]
            [JsonPropertyName("sent_at")]
            public DateTime SentAt { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string? Status { get; set; }
            
            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {
            [Required]
            [JsonPropertyName("automation_id")]
            public string? AutomationId { get; set; }

            [Required]
            [JsonPropertyName("file_name")]
            public string? FileName  { get; set; }

            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }

            [Required]
            [JsonPropertyName("recipient_email")]
            public string? RecipientEmail { get; set; }

            [Required]
            [JsonPropertyName("report_type")]
            public string? ReportType { get; set; }

            [Required]
            [JsonPropertyName("sent_at")]
            public DateTime SentAt { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string? Status { get; set; }
            
            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
            [Required]
            [JsonPropertyName("category")]
            public string? UpdatedAt { get; set; }
           
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [Required]
            [JsonPropertyName("automation_id")]
            public string? AutomationId { get; set; }

            [Required]
            [JsonPropertyName("file_name")]
            public string? FileName  { get; set; }

            [JsonPropertyName("file_url")]
            public string? FileUrl { get; set; }

            [Required]
            [JsonPropertyName("recipient_email")]
            public string? RecipientEmail { get; set; }

            [Required]
            [JsonPropertyName("report_type")]
            public string? ReportType { get; set; }

            [Required]
            [JsonPropertyName("sent_at")]
            public DateTime SentAt { get; set; }

            [Required]
            [JsonPropertyName("status")]
            public string? Status { get; set; }
            
            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
            [Required]
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [Required]
            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }
            
        }
    }
}

