using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        const string BlobContainerName = "studentpictures";

        readonly BlobServiceClient blobServiceClient;


        public BlobController(BlobServiceClient blobServiceClient)
        {
            this.blobServiceClient = blobServiceClient;
        }

        /// <summary>
        /// Get a file from the blob storage by its blobname
        /// </summary>
        /// <param name="blobName" example="avatar_04.png">The name from the file to get</param>
        /// <returns></returns>
        /// <response code="200">The file with the specified blobname is returned</response>
        /// <response code="404">The file with the specified blobname does not exist in the Database</response>
        [HttpGet("{blobName}")]
        public async Task<ActionResult> GetBlobAsync(string blobName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(BlobContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            if (blobClient == null)
            {
                return NotFound();
            }

            var stream = await blobClient.OpenReadAsync();
            var contentType = (await blobClient.GetPropertiesAsync()).Value.ContentType;

            return File(stream, contentType);

            //  or return image (to try in mudblazor)
            //Image blobImage = Image.FromStream(stream);
            //return blobImage;
        }

        /// <summary>
        /// Get an url from a file in the blob storage by its blobname
        /// </summary>
        /// <param name="blobName" example="avatar_04.png">The name from the file to get</param>
        /// <returns></returns>
        /// <response code="200">The url from the file with the specified blobname is returned</response>
        /// <response code="404">The file with the specified blobname does not exist in the Database</response>
        [HttpGet("Uri/{blobName}")]
        public ActionResult<Uri> GetBlobUri(string blobName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(BlobContainerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            if (blobClient == null)
            {
                return NotFound();
            }

            var imageUri = blobClient.Uri;
            return imageUri;
        }
    }
}
