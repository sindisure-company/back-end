using ApiSindisure.Domain.Interfaces.Apps.Audit;
using ApiSindisure.Domain.ViewModel.Audit;
using ApiSindisure.Services.Supabase;
using Newtonsoft.Json;


namespace ApiSindisure.Apps.Audit
{
    public class AuditApp : IAuditApp
    {
        private readonly SupabaseService _supabaseService;

        public AuditApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<AuditViewModel.Response> LogAuditAsync(AuditViewModel.LogRequest request, CancellationToken cancellationToken)
        {
            try
            {
               var client = _supabaseService.GetClient();

               var model = new AuditLogModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = request.UserId,
                        ContextoAudit = request.ContextoAudit,
                        GeneralInformations = JsonConvert.SerializeObject(request.GeneralInformations),
                        IpAddress = request.IpAddress,
                        UserAgent = request.UserAgent,
                        SessionId = request.SessionId,                     
                        CreatedAt = DateTime.UtcNow                   
                    };

                    var result = await client
                        .From<AuditLogModel>()
                        .Insert(model);

                    var createdModel = result.Models.First();

                return new AuditViewModel.Response
                {
                        Id = createdModel.Id,
                        UserId = createdModel.UserId,
                        ContextoAudit = createdModel.ContextoAudit,
                        GeneralInformations = createdModel.GeneralInformations,
                        IpAddress = createdModel.IpAddress,
                        UserAgent = createdModel.UserAgent,
                        SessionId = createdModel.SessionId,                     
                        CreatedAt = createdModel.CreatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar conta a pagar", ex);
            }
        }

        public async Task<List<AuditViewModel.Response>> GetAuditLogsAsync(AuditViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string jwt = string.Empty;
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
}

