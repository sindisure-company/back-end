[Supabase.Postgrest.Attributes.Table("user_plans")]
public class UserPlansModel : Supabase.Postgrest.Models.BaseModel
{
    [Supabase.Postgrest.Attributes.PrimaryKey("id")]
    public string Id { get; set; }

    [Supabase.Postgrest.Attributes.Column("user_id")]
    public string UserId { get; set; }

    [Supabase.Postgrest.Attributes.Column("plan_name")]
    public string PlanName { get; set; }

    [Supabase.Postgrest.Attributes.Column("plan_value")]
    public decimal PlanValue { get; set; }

    [Supabase.Postgrest.Attributes.Column("payment_date")]
    public string PaymentDate { get; set; }

    [Supabase.Postgrest.Attributes.Column("status")]
    public string? Status { get; set; }

    [Supabase.Postgrest.Attributes.Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Supabase.Postgrest.Attributes.Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}