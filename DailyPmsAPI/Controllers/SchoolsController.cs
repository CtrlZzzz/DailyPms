using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Data;
using DailyPmsShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        readonly ISchoolRepository schoolRepository;

        public SchoolsController(ISchoolRepository schoolRepo)
        {
            schoolRepository = schoolRepo;
        }


        /// <summary>
        /// Get a list of all schools
        /// </summary>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code="200">A list of schools is returned</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetALlSchoolsAsync()
        {
            var schools = await schoolRepository.GetAllSchoolsAsync();

            return Ok(schools);
        }

        /// <summary>
        /// Get a school by its ID
        /// </summary>
        /// <param name="id">The ID from the school to get</param>
        /// <returns></returns>
        /// <response code="200">The school with the specified ID is returned</response>
        /// <response code="404">The school with the specified ID does not exist in the Database</response>
        [HttpGet("{id:length(24)}", Name = "GetSchoolById")]
        public async Task<ActionResult<School>> GetSchoolByIdAsync(string id)
        {
            var school = await schoolRepository.GetSchoolByIdAsync(id);
            if (school == null)
            {
                return NotFound($"Could not find School with id = {id}");
            }

            return Ok(school);
        }

        /// <summary>
        /// Get a school by its Name or Moniker
        /// </summary>
        /// <param name="name">The Name or Moniker from the school to get</param>
        /// <returns></returns>
        /// <response code="200">The school with the specified Name or Moniker is returned</response>
        /// <response code="404">The school with the specified Name or Moniker does not exist in the Database</response>
        [HttpGet("ByName/{name}", Name = "GetSchoolByNameAsync")]
        public async Task<ActionResult<School>> GetSchoolByNameAsync(string name)
        {
            var school = await schoolRepository.GetSchoolByNameAsync(name);
            if (school == null)
            {
                return NotFound($"Could not find School with id = {name}");
            }

            return Ok(school);
        }

        /// <summary>
        /// Update a school
        /// </summary>
        /// <param name="id">The ID from the school to update</param>
        /// <param name="updatedSchool">The updated school object (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="204">The school with the specified ID is updated - No content is returned</response>
        /// <response code="404">The school with the specified ID does not exist in the Database</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateSchoolByIdAsync(string id, School updatedSchool)
        {
            var original = await schoolRepository.GetSchoolByIdAsync(id);
            if (original == null)
            {
                return NotFound($"Could not find School with id = {id}");
            }

            await schoolRepository.UpdateSchoolByIdAsync(id, updatedSchool);

            return NoContent();
        }

        /// <summary>
        /// Create a new school
        /// </summary>
        /// <param name="newSchool">The new school to create (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="201">The new school is created</response>
        /// <response code="400">A school with the same name as the new school's name already exists in the Database</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateSchoolAsync(School newSchool)
        {
            var alreadyExistingSchool = await schoolRepository.GetSchoolByNameAsync(newSchool.Name);
            if (alreadyExistingSchool != null)
            {
                return BadRequest($"A School with the name '{newSchool.Name}' already exists in the Database");
            }

            await schoolRepository.CreateSchoolAsync(newSchool);

            return CreatedAtRoute(nameof(GetSchoolByNameAsync), new { name = newSchool.Name }, newSchool);
        }

        /// <summary>
        /// Remove a school
        /// </summary>
        /// <param name="id">The ID from the school to remove from the Database</param>
        /// <returns></returns>
        /// <response code="204">The school with the specified ID is removed from the Database - No content is returned</response>
        /// <response code="404">The school with the specified ID does not exist in the Database</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteSchoolByIdAsync(string id)
        {
            var school = await schoolRepository.GetSchoolByIdAsync(id);
            if (school == null)
            {
                return NotFound($"Could not find school with id = {id}");
            }

            await schoolRepository.DeleteSchoolByIdAsync(id);

            return NoContent();
        }
    }
}
