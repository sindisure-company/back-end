[Supabase.Postgrest.Attributes.Table("user_details")]
public class UserDetailsModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("auth_user_id")]
    public string AuthUserId { get; set; }

    [Supabase.Postgrest.Attributes.Column("email")]
    public string Email { get; set; }

    [Supabase.Postgrest.Attributes.Column("address")]
    public string? Address { get; set; }

    [Supabase.Postgrest.Attributes.Column("avatar_url")]
    public string? AvatarUrl { get; set; }

    [Supabase.Postgrest.Attributes.Column("city")]
    public string? City { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Supabase.Postgrest.Attributes.Column("document_number")]
    public string? DocumentNumber { get; set; }

    [Supabase.Postgrest.Attributes.Column("file_name")]
    public string? FileName { get; set; }

    [Supabase.Postgrest.Attributes.Column("file_url")]
    public string? FileUrl { get; set; }

    [Supabase.Postgrest.Attributes.Column("first_name")]
    public string? FirstName { get; set; }

    [Supabase.Postgrest.Attributes.Column("img_avatar")]
    public string? ImgAvatar { get; set; }

    [Supabase.Postgrest.Attributes.Column("last_name")]
    public string? LastName { get; set; }

    [Supabase.Postgrest.Attributes.Column("neighborhood")]
    public string? Neighborhood { get; set; }

    [Supabase.Postgrest.Attributes.Column("number")]
    public string? Number { get; set; }

    [Supabase.Postgrest.Attributes.Column("phone")]
    public string? Phone { get; set; }

    [Supabase.Postgrest.Attributes.Column("state")]
    public string? State { get; set; }

    [Supabase.Postgrest.Attributes.Column("updated_at")]
    public DateTime UpdatedAt { get; set; }  
}
