using ApiSindisure.Domain.Interfaces.Apps.Buildings;
using ApiSindisure.Domain.ViewModel.Buildings;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.Buildings
{
    public class BuildingsApp : IBuildingsApp
    {
        private readonly SupabaseService _supabaseService;
        private readonly ILogger<BuildingsApp> _logger;
        public string LogId { get; set; }

        public BuildingsApp(SupabaseService supabaseService, ILogger<BuildingsApp> logger)
        {
            _supabaseService = supabaseService;
            _logger = logger;
            LogId = Guid.NewGuid().ToString();
        }

        public async Task<List<BuildingsViewModel.Response>> GetBuildingsAsync(BuildingsViewModel.GetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id))
                    throw new ArgumentException("BuildingsId não pode ser nulo ou vazio.");

                 if (!Guid.TryParse(request.Id, out _))
                    throw new Exception("BuildingsId inválido. Deve ser um UUID.");
                    
                var client = _supabaseService.GetClient();               

                var result = await client
                    .From<BuildingsModel>()
                    .Select("*")
                    .Filter("condominium_id", Supabase.Postgrest.Constants.Operator.Equals, request.Id)
                    .Order("created_at", Supabase.Postgrest.Constants.Ordering.Ascending)
                    .Get();

                return result.Models.Select(model => new BuildingsViewModel.Response
                {
                    Id = model.Id,
                    CondominiumId = model.CondominiumId,
                    Email = model.Email,
                    Name = model.Name,
                    Observation = model.Observation,
                    PersonalContact = model.PersonalContact,
                    Phone = model.Phone,
                    UnitNumber = model.UnitNumber,
                    Status = model.Status,
                    CreatedBy = model.CreatedBy,                 
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BuildingsApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao buscar contas a pagar", ex);
            }
        }

        public async Task<BuildingsViewModel.Response> CreateBuildingsAsync(BuildingsViewModel.CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                var client = _supabaseService.GetClient();
                var model = new BuildingsModel
                {
                    CondominiumId = request.CondominiumId,
                    Observation = request.Observation,
                    PersonalContact = request.PersonalContact,
                    Phone = request.Phone,
                    UnitNumber = request.UnitNumber,
                    Status = request.Status,
                    CreatedBy = request.CreatedBy,                 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<BuildingsModel>()
                    .Insert(model);

                var createdModel = result.Models.First();

                return new BuildingsViewModel.Response
                {
                    Id = createdModel.Id,
                    CondominiumId = model.CondominiumId,
                    Email = createdModel.Email,
                    Name = createdModel.Name,
                    Observation = createdModel.Observation,
                    PersonalContact = createdModel.PersonalContact,
                    Phone = createdModel.Phone,
                    UnitNumber = model.UnitNumber,
                    Status = createdModel.Status,
                    CreatedBy = createdModel.CreatedBy,                 
                    CreatedAt = createdModel.CreatedAt,
                    UpdatedAt = createdModel.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BuildingsApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao criar condominio", ex);
            }
        }

        public async Task<BuildingsViewModel.Response> UpdateBuildingsAsync(BuildingsViewModel.UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var model = new BuildingsModel
                {
                    Id = request.Id,
                    CondominiumId = request.CondominiumId,                    
                    Observation = request.Observation,
                    PersonalContact = request.PersonalContact,
                    Phone = request.Phone,
                    UnitNumber = request.UnitNumber,
                    Status = request.Status,
                    CreatedBy = request.CreatedBy,                 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await client
                    .From<BuildingsModel>()
                    .Where(x => x.Id == request.Id)
                    .Update(model);

                var updatedModel = result.Models.First();

                return new BuildingsViewModel.Response
                {
                    Id = updatedModel.Id,   
                    CondominiumId = request.CondominiumId,         
                    Email = updatedModel.Email,
                    Name = updatedModel.Name,
                    Observation = updatedModel.Observation,
                    PersonalContact = updatedModel.PersonalContact,
                    Phone = updatedModel.Phone,
                    UnitNumber = model.UnitNumber,
                    Status = updatedModel.Status,
                    CreatedBy = updatedModel.CreatedBy,                 
                    CreatedAt = updatedModel.CreatedAt,
                    UpdatedAt = updatedModel.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BuildingsApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao atualizar condominio", ex);
            }
        }

        public async Task DeleteBuildingsAsync(BuildingsViewModel.DeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                await client
                    .From<BuildingsModel>()
                    .Where(x => x.Id == request.Id)
                    .Delete();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(BuildingsApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao excluir condominio", ex);
            }
        }
    }    
}

