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
        [HttpGet("ByName/{name}", Name = "GetCenterByNameAsync")]
        public async Task<ActionResult<PmsCenter>> GetCenterByNameAsync(string name)
        {
            var center = await centerRepository.GetCenterByNameAsync(name);
            if (center == null)
            {
                return NotFound($"Could not find Pms Center with the name '{name}'");
            }

            return Ok(center);
        }

        /// <summary>
        /// Create a new PMS Center
        /// </summary>
        /// <param name="newCenter">The new center to create (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="201">The new center is created</response>
        /// <response code="400">A center with the same name as the new center's name already exists in the Database</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateCenterAsync(PmsCenter newCenter)
        {
            var alreadyExistingCenter = await centerRepository.GetCenterByNameAsync(newCenter.Name);
            if (alreadyExistingCenter != null)
            {
                return BadRequest($"A center with the name '{newCenter.Name}' already exists in the Database.");
            }

            await centerRepository.CreateCenterAsync(newCenter);

            return CreatedAtRoute(nameof(GetCenterByNameAsync), new { name = newCenter.Name }, newCenter);
        }

        /// <summary>
        /// Update a center
        /// </summary>
        /// <param name="id">The ID from the center to update</param>
        /// <param name="updatedCenter">The updated center object (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="204">The center with the specified ID is updated - No content is returned</response>
        /// <response code="404">The center with the specified ID does not exist in the Database</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateCenterByIdAsync(string id, PmsCenter updatedCenter)
        {
            var original = await centerRepository.GetCenterByIdAsync(id);
            if (original == null)
            {
                return NotFound($"Could not find center with id = {id}");
            }

            await centerRepository.UpdateCenterByIdAsync(id, updatedCenter);

            return NoContent();
        }

        /// <summary>
        /// Remove a center
        /// </summary>
        /// <param name="id">The ID from the center to remove from the Database</param>
        /// <returns></returns>
        /// <response code="204">The center with the specified ID is removed from the Database - No content is returned</response>
        /// <response code="404">The center with the specified ID does not exist in the Database</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteCenterByIdAsync(string id)
        {
            var center = await centerRepository.GetCenterByIdAsync(id);
            if (center == null)
            {
                return NotFound($"Could not find center with id = {id}");
            }

            await centerRepository.DeleteCenterByIdAsync(id);

            return NoContent();
        }
    }
}
