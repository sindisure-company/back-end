// Modelo para mapeamento com Supabase
    [Supabase.Postgrest.Attributes.Table("accounts_payable")]
    public class AccountsPayableModel : Supabase.Postgrest.Models.BaseModel
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public string Id { get; set; }

        [Supabase.Postgrest.Attributes.Column("description")]
        public string Description { get; set; }

        [Supabase.Postgrest.Attributes.Column("amount")]
        public decimal Amount { get; set; }

        [Supabase.Postgrest.Attributes.Column("due_date")]
        public DateTime DueDate { get; set; }

        [Supabase.Postgrest.Attributes.Column("status")]
        public string Status { get; set; }

        [Supabase.Postgrest.Attributes.Column("company")]
        public string? Company { get; set; }

        [Supabase.Postgrest.Attributes.Column("invoice_number")]
        public string? InvoiceNumber { get; set; }

        [Supabase.Postgrest.Attributes.Column("category")]
        public string Category { get; set; }

        [Supabase.Postgrest.Attributes.Column("notes")]
        public string? Notes { get; set; }

        [Supabase.Postgrest.Attributes.Column("condominium_id")]
        public string CondominiumId { get; set; }

        [Supabase.Postgrest.Attributes.Column("companies_recurring_id")]
        public string? CompaniesRecurringId { get; set; }

        [Supabase.Postgrest.Attributes.Column("created_by")]
        public string CreateBy { get; set; }

        [Supabase.Postgrest.Attributes.Column("file_name")]
        public string? FileName { get; set; }

        [Supabase.Postgrest.Attributes.Column("file_url")]
        public string? FileUrl { get; set; }

        [Supabase.Postgrest.Attributes.Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Supabase.Postgrest.Attributes.Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }