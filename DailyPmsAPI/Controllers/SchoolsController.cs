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

        [HttpGet]
        public ActionResult<IEnumerable<School>> GetALlSchools()
        {
            var schools = schoolRepository.GetAllSchools();

            return Ok(schools);
        }

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
