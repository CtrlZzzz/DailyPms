using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Data;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PmsCenterController: ControllerBase
    {
        readonly IPmsCenterRepository centerRepository;

        public PmsCenterController(IPmsCenterRepository centerRepo)
        {
            centerRepository = centerRepo;
        }

        /// <summary>
        /// Get a list of all PMS Centers
        /// </summary>
        /// <returns></returns>
        /// <response code="200">A list of PMS Centers is returned</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PmsCenter>>> GetAllCentersAsync()
        {
            var centers = await centerRepository.GetAllCentersAsync();

            return Ok(centers);
        }


    }
}
