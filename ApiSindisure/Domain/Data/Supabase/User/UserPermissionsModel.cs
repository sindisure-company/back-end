[Supabase.Postgrest.Attributes.Table("user_permissions")]
public class UserPermissionsModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("role")]
    public string? Roles { get; set; }

    [Supabase.Postgrest.Attributes.Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Supabase.Postgrest.Attributes.Column("user_id")]
    public string? UserId { get; set; }
}