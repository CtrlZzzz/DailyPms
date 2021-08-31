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

        /// <summary>
        /// Get a center by its ID
        /// </summary>
        /// <param name="id">The ID from the center to get</param>
        /// <returns></returns>
        /// <response code="200">The center with the specified ID is returned</response>
        /// <response code="404">The center with the specified ID does not exist in the Database</response>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PmsCenter>> GetCenterByIdAsync(string id)
        {
            var center = await centerRepository.GetCenterByIdAsync(id);
            if (center == null)
            {
                return NotFound($"Could not find center with id = {id}");
            }

            return Ok(center);
        }

        /// <summary>
        /// Get a center by its Name
        /// </summary>
        /// <param name="name">The Name of the classe to get</param>
        /// <returns></returns>
        /// <response code="200">The center with the specified Name is returned</response>
        /// <response code="404">The center with the specified Name does not exist in the Database</response>
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<PmsCenter>> GetCenterByNameAsync(string name)
        {
            var center = await centerRepository.GetCenterByNameAsync(name);
            if (center == null)
            {
                return NotFound($"Could not find Pms Center with the name '{name}'");
            }

            return Ok(center);
        }
    }
}
