using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Data;
using DailyPmsAPI.Models;
using Microsoft.AspNetCore.Http;
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
        /// <response code="500">There is a database internal error - Retry later or contact an administrator</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetALlSchoolsAsync()
        {
            try
            {
                var schools = await schoolRepository.GetAllSchoolsAsync();

                return Ok(schools);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }

        /// <summary>
        /// Get a school by its ID
        /// </summary>
        /// <param name="id">The ID from the school to get</param>
        /// <returns></returns>
        /// <response code="200">The school with the specified ID is returned</response>
        /// <response code="404">The school with the specified ID does not exist in the Database</response>
        /// <response code="500">There is a database internal error - Retry later or contact an administrator</response>
        [HttpGet("{id:length(24)}", Name = "GetSchoolById")]
        public async Task<ActionResult<School>> GetSchoolByIdAsync(string id)
        {
            try
            {
                var school = await schoolRepository.GetSchoolByIdAsync(id);
                if (school == null)
                {
                    return NotFound($"Could not find School with id = {id}");
                }

                return Ok(school);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }

        /// <summary>
        /// Get a school by its Name or Moniker
        /// </summary>
        /// <param name="name">The Name or Moniker from the school to get</param>
        /// <returns></returns>
        /// <response code="200">The school with the specified Name or Moniker is returned</response>
        /// <response code="404">The school with the specified Name or Moniker does not exist in the Database</response>
        /// <response code="500">There is a database internal error - Retry later or contact an administrator</response>
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<School>> GetSchoolByNameAsync(string name)
        {
            try
            {
                var school = await schoolRepository.GetSchoolByNameAsync(name);
                if (school == null)
                {
                    return NotFound($"Could not find School with id = {name}");
                }

                return Ok(school);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }

        /// <summary>
        /// Update a school
        /// </summary>
        /// <param name="id">The ID from the school to update</param>
        /// <param name="updatedSchool">The updated school object (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="204">The school with the specified ID is updated - No content is returned</response>
        /// <response code="404">The school with the specified ID does not exist in the Database</response>
        /// <response code="500">Your Json can not be serialized or there is a database internal error</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateSchoolByIdAsync(string id, School updatedSchool)
        {
            try
            {
                var original = await schoolRepository.GetSchoolByIdAsync(id);
                if (original == null)
                {
                    return NotFound($"Could not find School with id = {id}");
                }

                await schoolRepository.UpdateSchoolByIdAsync(id, updatedSchool);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }
    }
}
