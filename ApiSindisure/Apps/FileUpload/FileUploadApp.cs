using ApiSindisure.Domain.Interfaces.Apps.FileUpload;
using ApiSindisure.Domain.ViewModel.FileUpload;
using ApiSindisure.Services.Supabase;

namespace ApiSindisure.Apps.FileUpload
{
    public class FileUploadApp : IFileUploadApp
    {
        private readonly SupabaseService _supabaseService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FileUploadApp> _logger;
        public string LogId { get; set; }

        public FileUploadApp(SupabaseService supabaseService, IConfiguration configuration, ILogger<FileUploadApp> logger)
        {
            _supabaseService = supabaseService;
            _configuration = configuration;
            _logger = logger;
            LogId = Guid.NewGuid().ToString();
        }

        public async Task<FileUploadViewModel.Response> FileUploadAsync(FileUploadViewModel.Request request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                var bucket = client.Storage.From("account-receipts");

                var filePath = $"/{Guid.NewGuid()}_{request.File.FileName}";

                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    await request.File.CopyToAsync(ms, cancellationToken);
                    fileBytes = ms.ToArray();
                }

                var result = await bucket.Upload(fileBytes, filePath, new Supabase.Storage.FileOptions
                {
                    CacheControl = "3600",
                    Upsert = false
                });

                var baseUrl = _configuration.GetValue<string>("Supabase:Url");
                var path = _configuration.GetValue<string>("Supabase:Storage");
                var publicUrl = baseUrl + path + "account-receipts" + filePath;

                return new FileUploadViewModel.Response
                {
                    FilePath = filePath,
                    PublicUrl = publicUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(FileUploadApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao fazer upload do arquivo", ex);
            }
        }

        public async Task<FileUploadViewModel.Response> DownloadFileAsync(FileUploadViewModel.Download request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();
                            var bucket = client.Storage.From("account-receipts");

                            var prefix = "account-receipts/";
                            var relativeFilePath = request.FilePath.Contains(prefix)
                                ? request.FilePath.Substring(request.FilePath.IndexOf(prefix) + prefix.Length)
                                : request.FilePath;

                            var fileBytes = await bucket.Download(relativeFilePath, (EventHandler<float>?)null);

                            var base64String = Convert.ToBase64String(fileBytes);

                            return new FileUploadViewModel.Response
                            {
                                FileResponse = base64String,
                                FileName = relativeFilePath
                            };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(FileUploadApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                throw new Exception("Erro ao fazer download do arquivo", ex);
            }
            
        }    
        
        public async Task<bool> DeleteFileAsync(FileUploadViewModel.Delete request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _supabaseService.GetClient();

                var bucket = client.Storage.From("account-receipts");

                var prefix = "account-receipts/";
                var relativeFilePath = request.FilePath.Contains(prefix)
                    ? request.FilePath.Substring(request.FilePath.IndexOf(prefix) + prefix.Length)
                    : request.FilePath;

                    var response = await bucket.Remove(new List<string> { relativeFilePath });

                    return response != null && response.Count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(FileUploadApp)} - Erro ao acessar o banco de dados: {LogId}", ex);
                return false;
            }
        }

    }
}
