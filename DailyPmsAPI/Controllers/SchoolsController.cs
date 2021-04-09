using System;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<School>> GetALlSchools()
        {
            try
            {
                var schools = schoolRepository.GetAllSchools();

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
        public ActionResult<School> GetSchoolById(string id)
        {
            try
            {
                var school = schoolRepository.GetSchoolById(id);
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
        public ActionResult UpdateSchoolById(string id, School updatedSchool)
        {
            try
            {
                var original = schoolRepository.GetSchoolById(id);
                if (original == null)
                {
                    return NotFound($"Could not find School with id = {id}");
                }

                schoolRepository.UpdateSchoolById(id, updatedSchool);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }
    }
}
