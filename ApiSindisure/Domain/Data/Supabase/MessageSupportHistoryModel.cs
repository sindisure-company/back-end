[Supabase.Postgrest.Attributes.Table("message_support_history")]
public class MessageSupportHistoryModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("message")]
    public string? Message { get; set; }

    [Supabase.Postgrest.Attributes.Column("is_admin_response")]
    public bool IsAdminResponse { get; set; }

    [Supabase.Postgrest.Attributes.Column("support_message_id")]
    public string? SupportMessageId { get; set; }

    [Supabase.Postgrest.Attributes.Column("user_id")]
    public string? UserId { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }
}