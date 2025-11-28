using Microsoft.AspNetCore.Http;

namespace Nois.Application.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(string containerName, IFormFile file);
        Task<bool> DeleteFileAsync(string containerName, string blobName);
        string GetBlobUrl(string containerName, string blobName);
    }
}
