[Supabase.Postgrest.Attributes.Table("user_profiles")]
public class UserProfilesModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("user_id")]
    public string UserId { get; set; }

    [Supabase.Postgrest.Attributes.Column("first_name")]
    public string FirstName { get; set; }

    [Supabase.Postgrest.Attributes.Column("last_name")]
    public string LastName { get; set; }

    [Supabase.Postgrest.Attributes.Column("document_number")]
    public string DocumentNumber { get; set; }

    [Supabase.Postgrest.Attributes.Column("date_of_birth")]
    public string? DateOfBirth { get; set; }

    [Supabase.Postgrest.Attributes.Column("avatar_url")]
    public string? AvatarUrl { get; set; }

    [Supabase.Postgrest.Attributes.Column("address")]
    public string? Address { get; set; }

    [Supabase.Postgrest.Attributes.Column("number")]
    public string? Number { get; set; }

    [Supabase.Postgrest.Attributes.Column("neighborhood")]
    public string? Neighborhood { get; set; }

    [Supabase.Postgrest.Attributes.Column("city")]
    public string? City { get; set; }

    [Supabase.Postgrest.Attributes.Column("state")]
    public string? State { get; set; }

    [Supabase.Postgrest.Attributes.Column("phone")]
    public string? Phone { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Supabase.Postgrest.Attributes.Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}