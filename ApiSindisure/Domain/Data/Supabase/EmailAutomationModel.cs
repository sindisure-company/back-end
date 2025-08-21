[Supabase.Postgrest.Attributes.Table("email_automations")]
public class EmailAutomationModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("email")]
    public string? Email { get; set; }

    [Supabase.Postgrest.Attributes.Column("is_active")]
    public bool IsActive { get; set; }

    [Supabase.Postgrest.Attributes.Column("responsible_name")]
    public string? ResponsibleName { get; set; }

    [Supabase.Postgrest.Attributes.Column("send_cash_flow")]
    public bool SendCashFlow { get; set; }

    [Supabase.Postgrest.Attributes.Column("send_files")]
    public bool SendFiles { get; set; }

    [Supabase.Postgrest.Attributes.Column("user_id")]
    public string? UserId { get; set; }

    [Supabase.Postgrest.Attributes.Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }
}