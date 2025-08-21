using ApiSindisure.Domain.ViewModel.FileUpload;

namespace ApiSindisure.Domain.Interfaces.Apps.FileUpload
{
    public interface IFileUploadApp
    {
        Task<FileUploadViewModel.Response> FileUploadAsync(FileUploadViewModel.Request request, CancellationToken cancellationToken);
        Task<FileUploadViewModel.Response> DownloadFileAsync(FileUploadViewModel.Download request, CancellationToken cancellationToken);
        Task<bool> DeleteFileAsync(FileUploadViewModel.Delete request, CancellationToken cancellationToken);
    }
}

