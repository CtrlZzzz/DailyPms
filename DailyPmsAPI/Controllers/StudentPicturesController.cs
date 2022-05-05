using System;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentPicture>>> GetPicturesAsync()
        {
            return await dbContext.StudentPictures.ToListAsync();
        }

        // GET: api/Pictures/5
        [HttpGet("{id}", Name = "GetPicture")]
        public async Task<ActionResult<StudentPicture>> GetPictureByIdAsync(string id)
        {
            var studentPicture = await dbContext.StudentPictures.FindAsync(id);

            if (studentPicture == null)
            {
                return NotFound();
            }

            return studentPicture;
        }

        // PUT: api/Pictures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePictureByIdAsync(string id, StudentPicture updatedPicture)
        {
            if (id != updatedPicture._id)
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
        [HttpPost]
        public async Task<ActionResult<StudentPicture>> CreatePictureAsync(StudentPicture newStudentPicture)
        {
            dbContext.StudentPictures.Add(newStudentPicture);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentPictureExists(newStudentPicture._id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPicture", new { id = newStudentPicture._id }, newStudentPicture);
        }

        // DELETE: api/Pictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePictureAsync(string id)
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

        private bool StudentPictureExists(string id)
        {
            return dbContext.StudentPictures.Any(e => e._id == id);
        }
    }
}

