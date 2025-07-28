
[Supabase.Postgrest.Attributes.Table("condominium_details")]
public class BuildingsModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("condominium_id")]
    public string? CondominiumId { get; set; }
    [Supabase.Postgrest.Attributes.Column("name")]
    public string? Name { get; set; }

    [Supabase.Postgrest.Attributes.Column("unit_number")]
    public int UnitNumber { get; set; }

    [Supabase.Postgrest.Attributes.Column("email")]
    public string? Email { get; set; }

    [Supabase.Postgrest.Attributes.Column("phone")]
    public string? Phone { get; set; }

    [Supabase.Postgrest.Attributes.Column("personal_contact")]
    public string? PersonalContact { get; set; }

    [Supabase.Postgrest.Attributes.Column("observation")]
    public string? Observation { get; set; }

    [Supabase.Postgrest.Attributes.Column("status")]
    public string? Status { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_by")]
    public string? CreatedBy { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Supabase.Postgrest.Attributes.Column("update_at")]
    public DateTime UpdatedAt { get; set; }
}