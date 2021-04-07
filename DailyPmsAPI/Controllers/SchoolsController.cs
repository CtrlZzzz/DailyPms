using System.Collections.Generic;
using DailyPmsAPI.Data;
using DailyPmsAPI.Models;
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
        [HttpGet]
        public ActionResult<IEnumerable<School>> GetALlSchools()
        {
            var schools = schoolRepository.GetAllSchools();

            return Ok(schools);
        }

        /// <summary>
        /// Get a school with its ID
        /// </summary>
        /// <param name="id">The ID from the school to get</param>
        /// <returns></returns>
        /// <response code="200">The school with the specified ID is sent back by the API</response>
        /// <response code="404">The school with the specified ID does not exist in the Database</response>
        [HttpGet("{id:length(24)}", Name = "GetSchoolById")]
        public ActionResult<School> GetSchoolById(string id)
        {
            var school = schoolRepository.GetSchoolById(id);
            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }
    }
}
