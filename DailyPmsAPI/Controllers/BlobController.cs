using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
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

            //  or return image json (to try in mudblazor)
            //Image blobImage = Image.FromStream(stream);
            //return blobImage;
        }

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
