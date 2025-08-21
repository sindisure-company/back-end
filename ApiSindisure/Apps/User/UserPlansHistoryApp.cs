using ApiSindisure.Domain.Interfaces.Apps.UserPlansHistory;
using ApiSindisure.Domain.ViewModel.UserPlansHistoryViewModel;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.UserPlansHistory
{
    public class UserPlansHistoryApp : IUserPlansHistoryApp
    {
        private readonly SupabaseService _supabaseService;
        private readonly ILogger<UserPlansHistoryApp> _logger;
        public string LogId { get; set; }

        public UserPlansHistoryApp(SupabaseService supabaseService, ILogger<UserPlansHistoryApp> logger)
        {
            _supabaseService = supabaseService;
            _logger = logger;
            LogId = Guid.NewGuid().ToString();
        }

        public async Task<List<UserPlansHistoryViewModel.Response>> GetUserPlansHistoryAsync(UserPlansHistoryViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("UserPlansHistoryId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("UserPlansHistoryId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<UserPlansHistoryModel>()
                    .Select("*")
                    .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new UserPlansHistoryViewModel.Response
                {
                    Id = model.Id,
                    ActionType  = model.ActionType,
                    ChangedBy = model.ChangedBy,
                    PaymentDate = model.PaymentDate,
                    PlanName = model.PlanName,
                    PlanValue = model.PlanValue,
                    Status = model.Status,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,
                    UserId = model.UserId
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UserPlansHistoryApp)} - Erro ao acessar o banco de dados: {LogId}" + ex.Message, ex);
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<UserPlansHistoryViewModel.Response> CreateUserPlansHistoryAsync(UserPlansHistoryViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var client = _supabaseService.GetClient();
                var model = new UserPlansHistoryModel
                {
                    Id = Guid.NewGuid().ToString(),
                    ActionType = request.ActionType,
                    ChangedBy = request.ChangedBy,
                    PaymentDate = request.PaymentDate,
                    PlanName = request.PlanName,
                    PlanValue = request.PlanValue,
                    Status = request.Status,
                    UserPlanId = request.UserPlanId,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };

                var result = await client
                    .From<UserPlansHistoryModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new UserPlansHistoryViewModel.Response
                {
                    Id = createdModel.Id,
                    ActionType = createdModel.ActionType,
                    ChangedBy = createdModel.ChangedBy,
                    PaymentDate = createdModel.PaymentDate,
                    PlanName = createdModel.PlanName,
                    PlanValue = createdModel.PlanValue,
                    Status = createdModel.Status,
                    CreatedAt = createdModel.CreatedAt,
                    UpdatedAt = createdModel.UpdatedAt,
                    UserId = createdModel.UserId,
                    UserPlanId = createdModel.UserPlanId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UserPlansHistoryApp)} - Erro ao acessar o banco de dados: {LogId}" + ex.Message, ex);
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<UserPlansHistoryViewModel.Response> UpdateUserPlansHistoryAsync(UserPlansHistoryViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new UserPlansHistoryModel
                {
                    Id = request.Id,
                    ActionType = request.ActionType,
                    ChangedBy = request.ChangedBy,
                    PaymentDate = request.PaymentDate,
                    PlanName = request.PlanName,
                    PlanValue = request.PlanValue,
                    Status = request.Status,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = request.UserId,
                    UserPlanId = request.UserPlanId
                };               

                var result = await client
                    .From<UserPlansHistoryModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new UserPlansHistoryViewModel.Response
                {
                    Id = updatedModel.Id,
                    ActionType = updatedModel.ActionType,
                    ChangedBy = updatedModel.ChangedBy,
                    PaymentDate = updatedModel.PaymentDate,
                    PlanName = updatedModel.PlanName,
                    PlanValue = updatedModel.PlanValue,
                    Status = updatedModel.Status,
                    CreatedAt = updatedModel.CreatedAt,
                    UpdatedAt = updatedModel.UpdatedAt,
                    UserId = updatedModel.UserId,
                    UserPlanId = updatedModel.UserPlanId,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UserPlansHistoryApp)} - Erro ao acessar o banco de dados: {LogId}" + ex.Message, ex);
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteUserPlansHistoryAsync(UserPlansHistoryViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<UserPlansHistoryModel>()
                    .Where(x => x.Id == request.Id)
                    .Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UserPlansHistoryApp)} - Erro ao acessar o banco de dados: {LogId}" + ex.Message, ex);
                throw new Exception("Erro ao excluir condominio", ex);
            }
        }
    }    
}

