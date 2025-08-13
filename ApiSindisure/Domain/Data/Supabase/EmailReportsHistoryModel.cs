[Supabase.Postgrest.Attributes.Table("email_reports_history")]
public class EmailReportsHistoryModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("automation_id")]
    public string? AutomationId{ get; set; }

    [Supabase.Postgrest.Attributes.Column("file_name")]
    public string? FileName { get; set; }

    [Supabase.Postgrest.Attributes.Column("file_url")]
    public string? FileUrl { get; set; }

    [Supabase.Postgrest.Attributes.Column("recipient_email")]
    public string? RecipientEmail { get; set; }

    [Supabase.Postgrest.Attributes.Column("report_type")]
    public string? ReportType { get; set; }

    [Supabase.Postgrest.Attributes.Column("sent_at")]
    public DateTime SentAt { get; set; }

    [Supabase.Postgrest.Attributes.Column("status")]
    public string? Status { get; set; }

    [Supabase.Postgrest.Attributes.Column("user_id")]
    public string? UserId { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }
}