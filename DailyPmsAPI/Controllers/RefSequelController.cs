using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DailyPmsAPI.Sql;
using DailyPmsApi.Model;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefSequelController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public RefSequelController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: api/RefSequel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentPictureTest>>> GetStudentPictureTest()
        {
            return await _context.StudentPictureTest.ToListAsync();
        }

        // GET: api/RefSequel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentPictureTest>> GetStudentPictureTest(string id)
        {
            var studentPictureTest = await _context.StudentPictureTest.FindAsync(id);

            if (studentPictureTest == null)
            {
                return NotFound();
            }

            return studentPictureTest;
        }

        // PUT: api/RefSequel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentPictureTest(string id, StudentPictureTest studentPictureTest)
        {
            if (id != studentPictureTest._id)
            {
                return BadRequest();
            }

            _context.Entry(studentPictureTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentPictureTestExists(id))
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

        // POST: api/RefSequel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentPictureTest>> PostStudentPictureTest(StudentPictureTest studentPictureTest)
        {
            _context.StudentPictureTest.Add(studentPictureTest);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentPictureTestExists(studentPictureTest._id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentPictureTest", new { id = studentPictureTest._id }, studentPictureTest);
        }

        // DELETE: api/RefSequel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentPictureTest(string id)
        {
            var studentPictureTest = await _context.StudentPictureTest.FindAsync(id);
            if (studentPictureTest == null)
            {
                return NotFound();
            }

            _context.StudentPictureTest.Remove(studentPictureTest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentPictureTestExists(string id)
        {
            return _context.StudentPictureTest.Any(e => e._id == id);
        }
    }
}
