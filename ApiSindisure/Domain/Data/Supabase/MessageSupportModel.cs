[Supabase.Postgrest.Attributes.Table("message_support")]
public class MessageSupportModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("message")]
    public string? Message { get; set; }

    [Supabase.Postgrest.Attributes.Column("read")]
    public bool Read { get; set; }

    [Supabase.Postgrest.Attributes.Column("response")]
    public string? Response { get; set; }

    [Supabase.Postgrest.Attributes.Column("subject")]
    public string? Subject { get; set; }

    [Supabase.Postgrest.Attributes.Column("user_id")]
    public string? UserId { get; set; }

    [Supabase.Postgrest.Attributes.Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }
}