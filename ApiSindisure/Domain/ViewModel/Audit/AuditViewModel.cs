using System.ComponentModel.DataAnnotations;

namespace ApiSindisure.Domain.ViewModel.Audit
{
    public class AuditViewModel
    {
        public class LogRequest
        {
            public string? UserId { get; set; }
            
            [Required]
            public string ContextoAudit { get; set; }
            
            [Required]
            public object GeneralInformations { get; set; }
            
            public string? IpAddress { get; set; }
            public string? UserAgent { get; set; }
            public string? SessionId { get; set; }
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

