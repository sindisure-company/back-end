[Supabase.Postgrest.Attributes.Table("notification_history")]
public class NotificationHistoryModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("notification_key")]
    public string? NotificationKey { get; set; }

    [Supabase.Postgrest.Attributes.Column("notification_type")]
    public string? NotificationType { get; set; }

    [Supabase.Postgrest.Attributes.Column("viewed_at")]
    public DateTime ViewedAt { get; set; }

    [Supabase.Postgrest.Attributes.Column("user_id")]
    public string? UserId { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }
}