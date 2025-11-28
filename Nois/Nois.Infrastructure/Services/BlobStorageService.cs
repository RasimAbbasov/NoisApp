using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Nois.Application.Interfaces;

namespace Nois.Infrastructure.Services
{
    public class BlobStorageService : IBlobStorageService
    {

        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _baseUrl;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("AzureBlobSettings:DefaultConnection").Value;
            _baseUrl = configuration.GetSection("AzureBlobSettings:BaseUrl").Value;
            _blobServiceClient = new BlobServiceClient(connectionString); // create ONCE
        }
        public async Task<bool> DeleteFileAsync(string containerName, string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(blobName);

            var response = await blobClient.DeleteIfExistsAsync();

            return response.Value;  // returns true if deleted, false if blob didn't exist
        }

        public string GetBlobUrl(string containerName,string blobName)
        {
            return $"{_baseUrl}{containerName}/{blobName}";
        }

        public async Task<string> UploadFileAsync(string containerName, IFormFile file)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var blobClient = containerClient.GetBlobClient(blobName);

            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

            return blobName; // ✅ Return only the blob name
        }
    }
}
