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
    public class ClassesController : ControllerBase
    {
        readonly ISchoolRepository schoolRepository;
        readonly IClasseRepository classeRepository;

        public ClassesController(ISchoolRepository schoolRepo, IClasseRepository classeRepo)
        {
            schoolRepository = schoolRepo;
            classeRepository = classeRepo;
        }


        /// <summary>
        /// Get a list of all classes from a selected school
        /// </summary>
        /// <param name="schoolId">The ID from the school from which you want to get all classes</param>
        /// <remarks></remarks>
        /// <returns></returns>
        /// <response code="200">A list of classes is returned</response>
        /// <response code="404">The school from which you want to get the classes does not exist in the Database</response>
        [HttpGet("BySchool/{schoolId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Classe>>> GetAllClassesBySchoolAsync(string schoolId)
        {
            var school = await schoolRepository.GetSchoolByIdAsync(schoolId);
            if (school == null)
            {
                return NotFound($"Could not find School with id = {schoolId}");
            }

            var classes = await classeRepository.GetAllClassesBySchoolAsync(schoolId);

            return Ok(classes);
        }

        /// <summary>
        /// Get a classe by its ID
        /// </summary>
        /// <param name="id">The ID from the classe to get</param>
        /// <returns></returns>
        /// <response code="200">The classe with the specified ID is returned</response>
        /// <response code="404">The classe with the specified ID does not exist in the Database</response>
        [HttpGet("{id:length(24)}", Name = "GetClasseById")]
        public async Task<ActionResult<Classe>> GetClasseByIdAsync(string id)
        {
            var classe = await classeRepository.GetClasseByIdAsync(id);
            if (classe == null)
            {
                return NotFound($"Could not find classe with id = {id}");
            }

            return Ok(classe);
        }

        /// <summary>
        /// Get a classe by its Name
        /// </summary>
        /// <param name="name">The Name of the classe to get</param>
        /// <param name="schoolId">The school ID from the classe to get</param>
        /// <returns></returns>
        /// <response code="200">The classe with the specified Name is returned</response>
        /// <response code="404">The classe with the specified Name does not exist in the Database</response>
        [HttpGet("ByName/{name}/{schoolId:length(24)}", Name = "GetClasseByNameAsync")]
        public async Task<ActionResult<Classe>> GetClasseByNameAsync([FromQuery]string name, [FromQuery]string schoolId)
        {
            var classe = await classeRepository.GetClasseByNameAsync(name, schoolId);
            if (classe == null)
            {
                return NotFound($"Could not find classe with name = {name} in school with id = {schoolId}");
            }

            return Ok(classe);
        }

        /// <summary>
        /// Create a new classe
        /// </summary>
        /// <param name="newClasse">The new classe to create (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="201">The new classe is created</response>
        /// <response code="400">A classe with the same name as the new classe's name already exists in the Database</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateClasseAsync(Classe newClasse)
        {
            var alreadyExistingClasse = await classeRepository.GetClasseByNameAsync(newClasse.Name, newClasse.SchoolID);
            if (alreadyExistingClasse != null)
            {
                return BadRequest($"A Classe with the name '{newClasse.Name}' already exists for this school in the Database");
            }

            await classeRepository.CreateClasseAsync(newClasse);

            return CreatedAtRoute(nameof(GetClasseByNameAsync), new { name = newClasse.Name, schoolId = newClasse.SchoolID }, newClasse);
        }

        /// <summary>
        /// Update a classe
        /// </summary>
        /// <param name="id">The ID from the classe to update</param>
        /// <param name="updatedClasse">The updated classe object (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="204">The classe with the specified ID is updated - No content is returned</response>
        /// <response code="404">The classe with the specified ID does not exist in the Database</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateClasseByIdAsync(string id, Classe updatedClasse)
        {
            var original = await classeRepository.GetClasseByIdAsync(id);
            if (original == null)
            {
                return NotFound($"Could not find classe with id = {id}");
            }

            await classeRepository.UpdateClasseByIdAsync(id, updatedClasse);

            return NoContent();
        }

        /// <summary>
        /// Remove a classe
        /// </summary>
        /// <param name="id">The ID from the classe to remove from the Database</param>
        /// <returns></returns>
        /// <response code="204">The classe with the specified ID is removed from the Database - No content is returned</response>
        /// <response code="404">The classe with the specified ID does not exist in the Database</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteClasseByIdAsync(string id)
        {
            var classe = await classeRepository.GetClasseByIdAsync(id);
            if (classe == null)
            {
                return NotFound($"Could not find classe with id = {id}");
            }

            await classeRepository.DeleteClasseByIdAsync(id);

            return NoContent();
        }
    }
}
