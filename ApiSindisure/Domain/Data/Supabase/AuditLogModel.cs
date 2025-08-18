    [Supabase.Postgrest.Attributes.Table("general_audit")]
    public class AuditLogModel : Supabase.Postgrest.Models.BaseModel
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public string Id { get; set; }

        [Supabase.Postgrest.Attributes.Column("user_id")]
        public string? UserId { get; set; }

        [Supabase.Postgrest.Attributes.Column("contexto_audit")]
        public string ContextoAudit { get; set; }

        [Supabase.Postgrest.Attributes.Column("general_informations")]
        public string? GeneralInformations { get; set; }

        [Supabase.Postgrest.Attributes.Column("ip_address")]
        public string? IpAddress { get; set; }

        [Supabase.Postgrest.Attributes.Column("user_agent")]
        public string? UserAgent { get; set; }

        [Supabase.Postgrest.Attributes.Column("session_id")]
        public string? SessionId { get; set; }

        [Supabase.Postgrest.Attributes.Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }