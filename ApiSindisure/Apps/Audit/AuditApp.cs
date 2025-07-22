using ApiSindisure.Domain.Interfaces.Apps.Audit;
using ApiSindisure.Domain.ViewModel.Audit;
using ApiSindisure.Services.Supabase;
using System.Text.Json;

namespace ApiSindisure.Apps.Audit
{
    public class AuditApp : IAuditApp
    {
        private readonly SupabaseService _supabaseService;

        public AuditApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task LogAuditAsync(AuditViewModel.LogRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                
                // Usar RPC para chamar a função log_general_audit
                var parameters = new Dictionary<string, object>
                {
                    { "p_user_id", request.UserId },
                    { "p_contexto_audit", request.ContextoAudit },
                    { "p_general_informations", request.GeneralInformations },
                    { "p_ip_address", request.IpAddress },
                    { "p_user_agent", request.UserAgent },
                    { "p_session_id", request.SessionId }
                };

                await client.Rpc("log_general_audit", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao registrar auditoria", ex);
            }
        }

        public async Task<List<AuditViewModel.Response>> GetAuditLogsAsync(AuditViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var query = client.From<AuditLogModel>().Select("*");

                if (!string.IsNullOrEmpty(request.UserId))
                {
                    query = query.Where(x => x.UserId == request.UserId);
                }

                if (!string.IsNullOrEmpty(request.Context))
                {
                    query = query.Where(x => x.ContextoAudit == request.Context);
                }

                if (request.StartDate.HasValue)
                {
                    query = query.Where(x => x.CreatedAt >= request.StartDate.Value);
                }

                if (request.EndDate.HasValue)
                {
                    query = query.Where(x => x.CreatedAt <= request.EndDate.Value);
                }

                var result = await query
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Descending)
                    .Get();

                return result.Models.Select(model => new AuditViewModel.Response
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    ContextoAudit = model.ContextoAudit,
                    GeneralInformations = model.GeneralInformations,
                    IpAddress = model.IpAddress,
                    UserAgent = model.UserAgent,
                    SessionId = model.SessionId,
                    CreatedAt = model.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar logs de auditoria", ex);
            }
        }
    }

    // Modelo para mapeamento com Supabase
    [Supabase.Postgrest.Attributes.Table("audit_logs")]
    public class AuditLogModel : Supabase.Postgrest.Models.BaseModel
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public string Id { get; set; }

        [Supabase.Postgrest.Attributes.Column("user_id")]
        public string? UserId { get; set; }

        [Supabase.Postgrest.Attributes.Column("contexto_audit")]
        public string ContextoAudit { get; set; }

        [Supabase.Postgrest.Attributes.Column("general_informations")]
        public object GeneralInformations { get; set; }

        [Supabase.Postgrest.Attributes.Column("ip_address")]
        public string? IpAddress { get; set; }

        [Supabase.Postgrest.Attributes.Column("user_agent")]
        public string? UserAgent { get; set; }

        [Supabase.Postgrest.Attributes.Column("session_id")]
        public string? SessionId { get; set; }

        [Supabase.Postgrest.Attributes.Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}

