using DailyPmsAPI.BlobStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        readonly IBlobService blobService;

        public BlobController(IBlobService blobService)
        {
            this.blobService = blobService;
        }

        [HttpGet("{blobName}")]
        public async Task<ActionResult> GetBlob(string blobName)
        {
            var blob = await blobService.GetBlobAsync(blobName);

            return File(blob, blob.GetType().Name);
        }
    }
}
