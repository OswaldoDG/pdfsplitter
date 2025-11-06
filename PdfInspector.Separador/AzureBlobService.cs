using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Separador
{
    public static class AzureBlobService
    {
        public static async Task DownloadBlobAsync(string blobPath, string downloadFilePath, string connectionString, string containerName)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobPath);

            await blobClient.DownloadToAsync(downloadFilePath);
        }
    }
}
