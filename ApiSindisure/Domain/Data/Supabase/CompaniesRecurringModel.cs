
[Supabase.Postgrest.Attributes.Table("companies_recurring")]
public class CompaniesRecurringModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("company_id")]
    public string? CompanyId { get; set; }

    [Supabase.Postgrest.Attributes.Column("amount")]
    public decimal Amount { get; set; }

    [Supabase.Postgrest.Attributes.Column("category")]
    public string? Category { get; set; }

    [Supabase.Postgrest.Attributes.Column("condominium_id")]
    public string? CondominiumId { get; set; }

    [Supabase.Postgrest.Attributes.Column("description")]
    public string? Description { get; set; }

    [Supabase.Postgrest.Attributes.Column("due_day")]
    public int DueDay { get; set; }

    [Supabase.Postgrest.Attributes.Column("is_active")]
    public bool IsActive { get; set; }

    [Supabase.Postgrest.Attributes.Column("notes")]
    public string? Notes { get; set; }

    [Supabase.Postgrest.Attributes.Column("recurrence_type")]
    public string RecurrenceType { get; set; }


    [Supabase.Postgrest.Attributes.Column("created_by")]
    public string CreatedBy { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Supabase.Postgrest.Attributes.Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}