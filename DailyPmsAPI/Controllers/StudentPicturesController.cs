using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyPmsAPI.Sql;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyPmsAPI.Controllers
{
    [Route("api/Pictures")]
    [ApiController]
    public class StudentPicturesController : ControllerBase
    {
        private readonly SqlDbContext dbContext;

        public StudentPicturesController(SqlDbContext context)
        {
            dbContext = context;
        }

        // GET: api/Pictures
        /// <summary>
        /// Get a list of all StudentPictures
        /// </summary>
        /// <remarks>a "studentPicture" is a link stored in the database between a student an his avatar stored in the blobstorage</remarks>
        /// <returns></returns>
        /// <response code="200">A list of studentPictures is returned</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentPicture>>> GetPicturesAsync()
        {
            return await dbContext.StudentPictures.ToListAsync();
        }

        // GET: api/Pictures/5
        /// <summary>
        /// Get a studentPicture by student ID
        /// </summary>
        /// <param name="id" example="300000000000000000000003">the id from the student from which we want to retrieve the studentpicture</param>
        /// <returns></returns>
        /// <response code="200">The studentPicture from the student with the specified ID is returned</response>
        /// <response code="404">The studentPicture from the student with the specified ID does not exist in the Database</response>
        [HttpGet("{studentId}", Name = "GetPicture")]
        public async Task<ActionResult<StudentPicture>> GetPictureByStudentIdAsync(string studentId)
        {
            var studentPicture = await dbContext.StudentPictures.SingleOrDefaultAsync(p => p.StudentId == studentId);

            if (studentPicture == null)
            {
                return NotFound();
            }

            return studentPicture;
        }

        // PUT: api/Pictures/5
        /// <summary>
        /// Update a studentPicture
        /// </summary>
        /// <param name="id" example="5">The ID from the studentPicture to update</param>
        /// <param name="updatedPicture">The updated studentPicture object (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="204">The studentPicture with the specified ID is updated - No content is returned</response>
        /// <response code="400">The id from the studentPicture stored in the database is not the same as the id from the updated studentPicture</response>
        /// <response code="404">The studentPicture with the specified ID does not exist in the Database</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePictureByIdAsync(int id, StudentPicture updatedPicture)
        {
            if (id != updatedPicture.Id)
            {
                return BadRequest();
            }

            dbContext.Entry(updatedPicture).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentPictureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pictures
        /// <summary>
        /// Create a new studentPicture
        /// </summary>
        /// <param name="newStudentPicture">The new studentPicture to create (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="201">The new studentPicture is created</response>
        /// <response code="409">A studentPicture with the same Id as the new studentPicture's Id already exists in the Database</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<StudentPicture>> CreatePictureAsync(StudentPicture newStudentPicture)
        {
            dbContext.StudentPictures.Add(newStudentPicture);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentPictureExists(newStudentPicture.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPicture", new { id = newStudentPicture.Id }, newStudentPicture);
        }

        // DELETE: api/Pictures/5
        /// <summary>
        /// Remove a studentPicture
        /// </summary>
        /// <param name="id" example="5">The ID from the studentPicture to remove from the Database</param>
        /// <returns></returns>
        /// <response code="204">The studentPicture with the specified ID is removed from the Database - No content is returned</response>
        /// <response code="404">The studentPicture with the specified ID does not exist in the Database</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePictureAsync(int id)
        {
            var studentPicture = await dbContext.StudentPictures.FindAsync(id);
            if (studentPicture == null)
            {
                return NotFound();
            }

            dbContext.StudentPictures.Remove(studentPicture);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentPictureExists(int id)
        {
            return dbContext.StudentPictures.Any(e => e.Id == id);
        }
    }
}

