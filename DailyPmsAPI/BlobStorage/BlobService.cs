using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPmsAPI.BlobStorage
{
    public class BlobService : IBlobService
    {
        const string BlobContainerName = "studentpictures";

        readonly BlobServiceClient blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            this.blobServiceClient = blobServiceClient;
        }

        public async Task<Stream> GetBlobAsync(string blobName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(BlobContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            var blobDownload = new MemoryStream();
            var blobType = blobDownload.GetType().ToString();
            await blobClient.DownloadToAsync(blobDownload);
            blobDownload.Position = 0;

            var testTYpe = (await blobClient.GetPropertiesAsync()).Value.ContentType;

            return blobDownload;
            //var blobContent = new StreamReader(blobDownload).ReadToEnd();

            //return blobContent;

            //var properties = (await blobClient.GetPropertiesAsync()).Value;
            //var stream = await blobClient.OpenReadAsync();

            //return new File(stream, properties.ContentType);
        }

        public Task DeleteBlobAsync(string blobName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> ListBlobsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UploadContentBlobAsync(string content, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task UploadFileBlobAsync(string filePath, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
