using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Repositories;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PmsCenterController : ControllerBase
    {
        readonly PmsCenterRepository centerRepository;

        public PmsCenterController(IRepository<PmsCenter> centerRepo)
        {
            centerRepository = (PmsCenterRepository)centerRepo;
        }

        /// <summary>
        /// Get a list of all PMS Centers
        /// </summary>
        /// <returns></returns>
        /// <response code="200">A list of PMS Centers is returned</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PmsCenter>>> GetAllCentersAsync()
        {
            var centers = await centerRepository.GetAllAsync();

            return Ok(centers);
        }

        /// <summary>
        /// Get a center by its ID
        /// </summary>
        /// <param name="id" example="400000000000000000000001">The ID from the center to get</param>
        /// <returns></returns>
        /// <response code="200">The center with the specified ID is returned</response>
        /// <response code="404">The center with the specified ID does not exist in the Database</response>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PmsCenter>> GetCenterByIdAsync(string id)
        {
            var center = await centerRepository.GetByIdAsync(id);
            if (center == null)
            {
                return NotFound($"Could not find center with id = {id}");
            }

            return Ok(center);
        }

        /// <summary>
        /// Get a center by its Name
        /// </summary>
        /// <param name="name" example="Centre PMS n°1">The Name of the center to get</param>
        /// <returns></returns>
        /// <response code="200">The center with the specified Name is returned</response>
        /// <response code="404">The center with the specified Name does not exist in the Database</response>
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<PmsCenter>> GetCenterByNameAsync(string name)
        {
            var center = await centerRepository.GetByNameAsync(name);
            if (center == null)
            {
                return NotFound($"Could not find Pms Center with the name '{name}'");
            }

            return Ok(center);
        }

        /// <summary>
        /// Update a center
        /// </summary>
        /// <param name="id" example="400000000000000000000001">The ID from the center to update</param>
        /// <param name="updatedCenter">The updated pms center object (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="204">The center with the specified ID is updated - No content is returned</response>
        /// <response code="404">The center with the specified ID does not exist in the Database</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdatePmsCenterByIdAsync(string id, PmsCenter updatedCenter)
        {
            var original = await centerRepository.GetByIdAsync(id);
            if (original == null)
            {
                return NotFound($"Could not find center with id = {id}");
            }

            await centerRepository.UpdateAsync(id, updatedCenter);

            return NoContent();
        }

        /// <summary>
        /// Create a new center
        /// </summary>
        /// <param name="newCenter">The new Pms Center to create (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="201">The new center is created</response>
        /// <response code="400">A center with the same name as the new center's name already exists in the Database</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreatePmsCenterAsync(PmsCenter newCenter)
        {
            var alreadyExistingCenters = await centerRepository.GetAllAsync();
            foreach (var center in alreadyExistingCenters)
            {
                if (center.Name == newCenter.Name && center.PostalCode == newCenter.PostalCode)
                {
                    return BadRequest($"A pms center named {newCenter.Name} " +
                        $"in {newCenter.City} (postal code : {newCenter.PostalCode}) already exists in the Database !");
                }
            }

            await centerRepository.CreateAsync(newCenter);

            return CreatedAtRoute(nameof(GetCenterByNameAsync), new { name = newCenter.Name }, newCenter);
        }

        /// <summary>
        /// Remove a center
        /// </summary>
        /// <param name="id" example="400000000000000000000001">The ID from the center to remove from the Database</param>
        /// <returns></returns>
        /// <response code="204">The center with the specified ID is removed from the Database - No content is returned</response>
        /// <response code="404">The center with the specified ID does not exist in the Database</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeletePmsCenterByIdAsync(string id)
        {
            var school = await centerRepository.GetByIdAsync(id);
            if (school == null)
            {
                return NotFound($"Could not find center with id = {id}");
            }

            await centerRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
