
[Supabase.Postgrest.Attributes.Table("companies")]
public class CompaniesModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("name")]
    public string? Name { get; set; }

    [Supabase.Postgrest.Attributes.Column("personal_contact")]
    public string? PersonalContact { get; set; }

    [Supabase.Postgrest.Attributes.Column("phone")]
    public string? Phone { get; set; }

    [Supabase.Postgrest.Attributes.Column("email")]
    public string? Email { get; set; }   

    [Supabase.Postgrest.Attributes.Column("document")]
    public string? Document { get; set; }

    [Supabase.Postgrest.Attributes.Column("address")]
    public string? Address { get; set; }

    [Supabase.Postgrest.Attributes.Column("category")]
    public string? Category { get; set; }

    [Supabase.Postgrest.Attributes.Column("status")]
    public string? Status { get; set; }

    [Supabase.Postgrest.Attributes.Column("notes")]
    public string? Notes { get; set; }    

    [Supabase.Postgrest.Attributes.Column("created_by")]
    public string CreatedBy { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Supabase.Postgrest.Attributes.Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}