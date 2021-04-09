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
        /// <response code="500">There is a database internal error - Retry later or contact an administrator</response>
        [HttpGet("BySchool/{schoolId:length(24)}")]
        public ActionResult<IEnumerable<Classe>> GetAllClassesBySchool(string schoolId)
        {
            try
            {
                var school = schoolRepository.GetSchoolById(schoolId);
                if (school == null)
                {
                    return NotFound($"Could not find School with id = {schoolId}");
                }

                var classes = classeRepository.GetAllClassesBySchool(schoolId);

                return Ok(classes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }

        /// <summary>
        /// Get a classe by its ID
        /// </summary>
        /// <param name="id">The ID from the classe to get</param>
        /// <returns></returns>
        /// <response code="200">The classe with the specified ID is returned</response>
        /// <response code="404">The classe with the specified ID does not exist in the Database</response>
        /// <response code="500">There is a database internal error - Retry later or contact an administrator</response>
        [HttpGet("{id:length(24)}", Name = "GetClasseById")]
        public ActionResult<Classe> GetClasseById(string id)
        {
            try
            {
                var classe = classeRepository.GetClasseById(id);
                if (classe == null)
                {
                    return NotFound($"Could not find classe with id = {id}");
                }

                return Ok(classe);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }

        /// <summary>
        /// Get a classe by its Name
        /// </summary>
        /// <param name="name">The Name of the classe to get</param>
        /// <returns></returns>
        /// <response code="200">The classe with the specified Name is returned</response>
        /// <response code="404">The classe with the specified Name does not exist in the Database</response>
        /// <response code="500">There is a database internal error - Retry later or contact an administrator</response>
        [HttpGet("ByName/{name}", Name = "GetClasseByName")]
        public ActionResult<Classe> GetClasseByName(string name)
        {
            try
            {
                var classe = classeRepository.GetClasseByName(name);
                if (classe == null)
                {
                    return NotFound($"Could not find classe with name = {name}");
                }

                return Ok(classe);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }

        /// <summary>
        /// Create a new classe
        /// </summary>
        /// <param name="newClasse">The new classe to create (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="201">The new classe is created</response>
        /// <response code="400">A classe with the same name as the new classe's name already exists in the Database</response>
        /// <response code="500">Your Json can not be serialized or there is a database internal error</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult CreateClasse(Classe newClasse)
        {
            try
            {
                var alreadyExistingClasse = classeRepository.GetClasseByName(newClasse.Name);
                if (alreadyExistingClasse != null)
                {
                    return BadRequest($"A Classe with the name '{newClasse.Name}' already exists in the Database");
                }

                classeRepository.CreateClasse(newClasse);

                return CreatedAtRoute(nameof(GetClasseByName), new { Name = newClasse.Name }, newClasse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }

        /// <summary>
        /// Update a classe
        /// </summary>
        /// <param name="id">The ID from the classe to update</param>
        /// <param name="updatedClasse">The updated classe object (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="204">The classe with the specified ID is updated - No content is returned</response>
        /// <response code="404">The classe with the specified ID does not exist in the Database</response>
        /// <response code="500">Your Json can not be serialized or there is a database internal error</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateClasseById(string id, Classe updatedClasse)
        {
            try
            {
                var original = classeRepository.GetClasseById(id);
                if (original == null)
                {
                    return NotFound($"Could not find classe with id = {id}");
                }

                classeRepository.UpdateClasseById(id, updatedClasse);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }

        /// <summary>
        /// Remove a classe
        /// </summary>
        /// <param name="id">The ID from the classe to remove from the Database</param>
        /// <returns></returns>
        /// <response code="204">The classe with the specified ID is removed from the Database - No content is returned</response>
        /// <response code="404">The classe with the specified ID does not exist in the Database</response>
        /// <response code="500">There is a database internal error - Retry later or contact an administrator</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteClasseById(string id)
        {
            try
            {
                var classe = classeRepository.GetClasseById(id);
                if (classe == null)
                {
                    return NotFound($"Could not find classe with id = {id}");
                }

                classeRepository.DeleteClasseById(id);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database server error!");
            }
        }
    }
}
