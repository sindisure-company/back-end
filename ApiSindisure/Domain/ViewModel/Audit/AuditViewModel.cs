using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ApiSindisure.Domain.ViewModel.Audit
{
    public class AuditViewModel
    {
        public class LogRequest
        {
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }

            [Required]
            [JsonPropertyName("contexto_audit")]
            public string ContextoAudit { get; set; }

            [Required]
            [JsonPropertyName("general_informations")]
            public Dictionary<string, object> GeneralInformations { get; set; }

            [JsonPropertyName("ip_address")]
            public string? IpAddress { get; set; }

            [JsonPropertyName("user_agent")]
            public string? UserAgent { get; set; }

            [JsonPropertyName("session_id")]
            public string? SessionId { get; set; }
        }

        public class GeneralInformations
        {
            public string Action { get; set; }
            public Details Details { get; set; }
        }

        public class Details
        {
            public string Email { get; set; }
            public bool Success { get; set; }
        }

        public class GetRequest
        {
            public string? UserId { get; set; }
            public string? Context { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class Response
        {
            public string Id { get; set; }
            public string? UserId { get; set; }
            public string ContextoAudit { get; set; }
            public object GeneralInformations { get; set; }
            public string? IpAddress { get; set; }
            public string? UserAgent { get; set; }
            public string? SessionId { get; set; }
            public DateTime CreatedAt { get; set; }
        }        
    }
}

