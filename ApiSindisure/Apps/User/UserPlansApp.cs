using System.Security.Cryptography.X509Certificates;
using ApiSindisure.Domain.Interfaces.Apps.UserPlans;
using ApiSindisure.Domain.ViewModel.UserPlansViewModel;
using ApiSindisure.Services.Supabase;

using Supabase.Postgrest.Attributes;

namespace ApiSindisure.Apps.UserPlans
{
    public class UserPlansApp : IUserPlansApp
    {
        private readonly SupabaseService _supabaseService;

        public UserPlansApp(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        public async Task<List<UserPlansViewModel.Response>> GetUserPlansAsync(UserPlansViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("UserPlansId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("UserPlansId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<UserPlansModel>()
                    .Select("*")
                    .Filter("created_by", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new UserPlansViewModel.Response
                {
                    Id = model.Id,
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
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }
        
        public async Task<UserPlansViewModel.Response> GetUniqueUserPlansAsync(UserPlansViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("UserPlansId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("UserPlansId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<UserPlansModel>()
                    .Select("*")
                    .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                if (result.Models.Count == 0)
                    throw new Exception("Nenhum registro encontrado para o UserPlansId fornecido.");

                var resultPlan = result.Models.First();

                return new UserPlansViewModel.Response
                {
                    Id = resultPlan.Id,
                    PaymentDate = resultPlan.PaymentDate,
                    PlanName = resultPlan.PlanName,
                    PlanValue = resultPlan.PlanValue,
                    Status = resultPlan.Status,
                    CreatedAt = resultPlan.CreatedAt,
                    UpdatedAt = resultPlan.UpdatedAt,
                    UserId = resultPlan.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<UserPlansViewModel.Response> CreateUserPlansAsync(UserPlansViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new UserPlansModel
                {
                    Id = Guid.NewGuid().ToString(),
                    PaymentDate = request.PaymentDate,
                    PlanName = request.PlanName,
                    PlanValue = request.PlanValue,
                    Status = request.Status,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };

                var result = await client
                    .From<UserPlansModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new UserPlansViewModel.Response
                {
                    Id = createdModel.Id,
                    PaymentDate = createdModel.PaymentDate,
                    PlanName = createdModel.PlanName,
                    PlanValue = createdModel.PlanValue,
                    Status = createdModel.Status,
                    CreatedAt = createdModel.CreatedAt,
                    UpdatedAt = createdModel.UpdatedAt,
                    UserId = createdModel.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<UserPlansViewModel.Response> UpdateUserPlansAsync(UserPlansViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();

                var resultGet = await client
                    .From<UserPlansModel>()
                    .Select("*")
                    .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();
                
                var retornoGet = resultGet.Models.FirstOrDefault();

                var model = new UserPlansModel
                {
                    Id = retornoGet.Id,
                    PaymentDate = request.PaymentDate,
                    PlanName = request.PlanName,
                    PlanValue = request.PlanValue,
                    Status = request.Status,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = request.UserId,
                    CreatedAt = retornoGet.CreatedAt
                };               

                var result = await client
                    .From<UserPlansModel>()
                    .Where(x => x.Id == retornoGet.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new UserPlansViewModel.Response
                {
                    Id = updatedModel.Id,
                    PaymentDate = updatedModel.PaymentDate,
                    PlanName = updatedModel.PlanName,
                    PlanValue = updatedModel.PlanValue,
                    Status = updatedModel.Status,
                    UpdatedAt = updatedModel.UpdatedAt,
                    UserId = updatedModel.UserId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteUserPlansAsync(UserPlansViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<UserPlansModel>()
                    .Where(x => x.Id == request.Id)
                    .Delete();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir condominio", ex);
            }
        }
    }    
}

