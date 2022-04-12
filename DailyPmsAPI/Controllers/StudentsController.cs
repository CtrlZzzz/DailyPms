using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Repositories;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        readonly SchoolRepository schoolRepository;
        readonly StudentRepository studentRepository;
        //readonly IClasseRepository classeRepository;

        public StudentsController(IRepository<School> schoolRepo, IRepository<Student> studentRepo)
        {
            schoolRepository = (SchoolRepository)schoolRepo;
            studentRepository = (StudentRepository)studentRepo;
            //classeRepository = classeRepo;
        }


        /// <summary>
        /// Get a list of all students (from all schools)
        /// </summary>
        /// <returns></returns>
        /// <response code="200">A list of students is returned</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudentsAsync()
        {
            var students = await studentRepository.GetAllAsync();

            return Ok(students);
        }

        /// <summary>
        /// Get a list of all students from one school
        /// </summary>
        /// <param name="schoolId">The ID from the school from which you want to get all students</param>
        /// <returns></returns>
        /// <response code="200">A list of students is returned</response>
        /// <response code="404">The school from which you want to get the students does not exist in the Database</response>
        [HttpGet("BySchool/{schoolId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudentsBySchoolAsync(string schoolId)
        {
            var school =  await schoolRepository.GetByIdAsync(schoolId);
            if (school == null)
            {
                return NotFound($"Could not find the school with id = {schoolId}");
            }

            var students = await studentRepository.GetAllFromIdAsync(schoolId);

            return Ok(students);
        }

        /*
        /// <summary>
        /// Get a list of students from one classe
        /// </summary>
        /// <param name="classeId">The ID from the classe from which you want to get all students</param>
        /// <param name="schoolId">The ID from the school from which you want to get all students from a classe</param>
        /// <returns></returns>
        /// <response code="200">A list of students is returned</response>
        /// <response code="404">The school or the classe from which you want to get the students does not exist in the Database</response>
        [HttpGet("ByClasse/{classeId:length(24)}/{schoolId:length(24)}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudentsByClasseAsync([FromQuery] string classeId, [FromQuery] string schoolId)
        {
            var school = await schoolRepository.GetSchoolByIdAsync(schoolId);
            if (school == null)
            {
                return NotFound($"Could not find school with id = {schoolId}");
            }

            var classe = await classeRepository.GetClasseByIdAsync(classeId);
            if (classe == null)
            {
                return NotFound($"Could not find classe with id = {classeId} in school with id = {schoolId}");
            }

            var students = studentRepository.GetAllStudentsByClasseAsync(classeId, schoolId);

            return Ok(students);
        }
        */

        /// <summary>
        /// Get a student by its ID
        /// </summary>
        /// <param name="id">The ID from the student to get</param>
        /// <returns></returns>
        /// <response code="200">The student with the specified ID is returned</response>
        /// <response code="404">The student with the specified ID does not exist in the Database</response>
        [HttpGet("{id:length(24)}", Name = "GetStudentByIdAsync")]
        public async Task<ActionResult<Student>> GetStudentByIdAsync(string id)
        {
            var student = await studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Could not find student with id = {id} in the Database");
            }

            return Ok(student);
        }

        /// <summary>
        /// Get one (or more) student(s) by its (their) Name
        /// </summary>
        /// <param name="name">The name of the student to get</param>
        /// <returns></returns>
        /// <response code="200">A list of students with the specified Name is returned</response>
        /// <response code="404">The student(s) with the specified Name does not exist in the Database</response>
        [HttpGet("ByName/{name}", Name = "GetStudentByNameAsync")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentByNameAsync(string name)
        {
            var students = await studentRepository.GetByNameAsync(name);
            if (students == null)
            {
                return NotFound($"Could not find student with name containing {name} in the Database");
            }

            return Ok(students);
        }

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="newStudent">The new student to create (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="200">The new student is created</response>
        /// <response code="400">A student with the same firstName, lastName and birthDate as the new student already exists in the Database</response>
        [HttpPost]
        public async Task<ActionResult> CreateStudentAsync(Student newStudent)
        {
            var alreadyExistingStudents = await studentRepository.GetByNameAsync(newStudent.LastName);
            foreach (var student in alreadyExistingStudents)
            {
                if (DateTime.Equals(student.BirthDate, newStudent.BirthDate) && student.FirstName == newStudent.FirstName)
                {
                    return BadRequest($"A Student named {newStudent.FirstName} {newStudent.LastName} and born {newStudent.BirthDate} already exists in the Database");
                }
            }

            await studentRepository.CreateAsync(newStudent);

            return Ok(newStudent);
        }

        /// <summary>
        /// Update a student
        /// </summary>
        /// <param name="id">The ID from the student to update</param>
        /// <param name="updatedStudent">The updated student object (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="204">The student with the specified ID is updated - No content is returned</response>
        /// <response code="404">The student with the specified ID does not exist in the Database</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateStudentByIdAsync(string id, Student updatedStudent)
        {
            var original = await studentRepository.GetByIdAsync(id);
            if (original == null)
            {
                return NotFound($"Could not find student with id = {id}");
            }

            await studentRepository.UpdateAsync(id, updatedStudent);

            return NoContent();
        }

        /// <summary>
        /// Remove a student
        /// </summary>
        /// <param name="id">The ID from the student to remove from the Database</param>
        /// <returns></returns>
        /// <response code="204">The student with the specified ID is removed from the Database - No content is returned</response>
        /// <response code="404">The student with the specified ID does not exist in the Database</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteStudentByIdAsync(string id)
        {
            var student = await studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Could not found student with id = {id}");
            }

            await studentRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
