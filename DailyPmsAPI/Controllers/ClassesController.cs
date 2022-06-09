using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Repositories;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        readonly SchoolRepository schoolRepository;
        readonly ClassRepository classRepository;

        public ClassesController(IRepository<School> schoolRepo, IRepository<Class> classeRepo)
        {
            schoolRepository = (SchoolRepository)schoolRepo;
            classRepository = (ClassRepository)classeRepo;
        }


        /// <summary>
        /// Get a list of all classes from a selected school
        /// </summary>
        /// <param name="schoolId" example="100000000000000000000004">The ID from the school from which you want to get all classes</param>
        /// <remarks>The word class refers to a school class object (from DailyPmsShared library in DailyPms project), not a c# class!</remarks>
        /// <returns></returns>
        /// <response code="200">A list of classes is returned</response>
        /// <response code="404">The school from which you want to get the classes does not exist in the Database</response>
        [HttpGet("BySchool/{schoolId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Class>>> GetAllClassesBySchoolAsync(string schoolId)
        {
            var school = await schoolRepository.GetByIdAsync(schoolId);
            if (school == null)
            {
                return NotFound($"Could not find School with id = {schoolId}");
            }

            var classes = await classRepository.GetAllFromIdAsync(schoolId);

            return Ok(classes);
        }

        /// <summary>
        /// Get a class by its ID
        /// </summary>
        /// <param name="id" example="200000000000000000000001">The ID from the class to get</param>
        /// <remarks>The word class refers to a school class object (from DailyPmsShared library in DailyPms project), not a c# class!</remarks>
        /// <returns></returns>
        /// <response code="200">The class with the specified ID is returned</response>
        /// <response code="404">The class with the specified ID does not exist in the Database</response>
        [HttpGet("{id:length(24)}", Name = "GetClassById")]
        public async Task<ActionResult<Class>> GetClassByIdAsync(string id)
        {
            var classe = await classRepository.GetByIdAsync(id);
            if (classe == null)
            {
                return NotFound($"Could not find class with id = {id}");
            }

            return Ok(classe);
        }

        /// <summary>
        /// Get a class by its Name
        /// </summary>
        /// <param name="name" example="Accueil2">The Name of the class to get</param>
        /// <remarks>The word class refers to a school class object (from DailyPmsShared library in DailyPms project), not a c# class!</remarks>
        /// <returns></returns>
        /// <response code="200">The class with the specified Name is returned</response>
        /// <response code="404">The class with the specified Name does not exist in the Database</response>
        [HttpGet("ByName/{name}", Name = "GetClassByNameAsync")]
        public async Task<ActionResult<Class>> GetClassByNameAsync(string name)
        {
            var classe = await classRepository.GetByNameAsync(name);
            if (classe == null)
            {
                return NotFound($"Could not find class(es) with name = {name}");
            }

            return Ok(classe);
        }

        /// <summary>
        /// Create a new class
        /// </summary>
        /// <param name="newClass">The new class to create (passed in the request body)</param>
        /// <remarks>The word class refers to a school class object (from DailyPmsShared library in DailyPms project), not a c# class!</remarks>
        /// <returns></returns>
        /// <response code="201">The new class is created</response>
        /// <response code="400">A class with the same name as the new class's name already exists in the Database</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateClassAsync(Class newClass)
        {
            var alreadyExistingClass = await classRepository.GetByNameAsync(newClass.Name);
            if (alreadyExistingClass != null)
            {
                return BadRequest($"A Class with the name '{newClass.Name}' already exists in the Database");
            }

            await classRepository.CreateAsync(newClass);

            return CreatedAtRoute(nameof(GetClassByNameAsync), new { name = newClass.Name }, newClass);
        }

        /// <summary>
        /// Update a classe
        /// </summary>
        /// <param name="id" example="200000000000000000000001">The ID from the class to update</param>
        /// <param name="updatedClass">The updated class object (passed in the request body)</param>
        /// <remarks>The word class refers to a school class object (from DailyPmsShared library in DailyPms project), not a c# class!</remarks>
        /// <returns></returns>
        /// <response code="204">The class with the specified ID is updated - No content is returned</response>
        /// <response code="404">The class with the specified ID does not exist in the Database</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateClassByIdAsync(string id, Class updatedClass)
        {
            var original = await classRepository.GetByIdAsync(id);
            if (original == null)
            {
                return NotFound($"Could not find class with id = {id}");
            }

            await classRepository.UpdateAsync(id, updatedClass);

            return NoContent();
        }

        /// <summary>
        /// Remove a class
        /// </summary>
        /// <param name="id" example="200000000000000000000001">The ID from the class to remove from the Database</param>
        /// <remarks>The word class refers to a school class object (from DailyPmsShared library in DailyPms project), not a c# class!</remarks>
        /// <returns></returns>
        /// <response code="204">The class with the specified ID is removed from the Database - No content is returned</response>
        /// <response code="404">The class with the specified ID does not exist in the Database</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteClassByIdAsync(string id)
        {
            var classe = await classRepository.GetByIdAsync(id);
            if (classe == null)
            {
                return NotFound($"Could not find class with id = {id}");
            }

            await classRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
