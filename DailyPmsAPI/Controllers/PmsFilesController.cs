using System.Threading.Tasks;
using DailyPmsAPI.Repositories;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc;

namespace DailyPmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PmsFilesController : ControllerBase
    {
        readonly PmsFileRepository pmsFileRepository;

        public PmsFilesController(IRepository<PmsFile> fileRepo)
        {
            pmsFileRepository = (PmsFileRepository)fileRepo;
        }


        /// <summary>
        /// Get a PmsFile by a student ID
        /// </summary>
        /// <param name="studentId" example="300000000000000000000003">The ID from the pmsFile's student to get</param>
        /// <returns></returns>
        /// <response code="200">The PmsFile from the student with the specified ID is returned</response>
        /// <response code="404">The PmsFile from the student with the specified ID does not exist in the Database</response>
        [HttpGet("ByStudent/{studentId:length(24)}", Name = "GetPmsFileByStudentIdAsync")]
        public async Task<ActionResult<PmsFile>> GetPmsFileByStudentIdAsync(string studentId)
        {
            var pmsFile = await pmsFileRepository.GetByIdAsync(studentId);
            if (pmsFile == null)
            {
                return NotFound($"Could not find PmsFile from the Student with studentId = {studentId}");
            }

            return Ok(pmsFile);
        }

        /// <summary>
        /// Create a new PmsFile
        /// </summary>
        /// <param name="newPmsFile">The new PmsFile to create (passed in the request body)</param>
        /// <returns></returns>
        /// <response code="201">The new PmsFile is created</response>
        /// <response code="400">A PmsFile for the student with the ID specified in the new PmsFile already exists in the Database</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreatePmsFileAsync(PmsFile newPmsFile)
        {
            var alreadyExistingFile = pmsFileRepository.GetByIdAsync(newPmsFile.StudentID);
            if (alreadyExistingFile != null)
            {
                return BadRequest($"A PmsFile from the student with id = {newPmsFile.StudentID} already exists in the Database");
            }

            await pmsFileRepository.CreateAsync(newPmsFile);

            return CreatedAtRoute(nameof(GetPmsFileByStudentIdAsync), new { studentId = newPmsFile.StudentID }, newPmsFile);
        }

        /// <summary>
        /// Update a PmsFile
        /// </summary>
        /// <param name="studentId" example="300000000000000000000003">The ID from the PmsFile's student to update</param>
        /// <param name="updatedPmsFile"></param>
        /// <returns></returns>
        /// <response code="204">The PmsFile with the specified ID is updated - No content is returned</response>
        /// <response code="404">The PmsFile with the specified ID does not exist in the Database</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpDatePmsFileAsync(string studentId, PmsFile updatedPmsFile)
        {
            var original = pmsFileRepository.GetByIdAsync(studentId);
            if (original == null)
            {
                return NotFound($"Could not find PmsFile with id = {studentId}");
            }

            await pmsFileRepository.UpdateAsync(studentId, updatedPmsFile);

            return NoContent();
        }

        /// <summary>
        /// Delete a PmsFile
        /// </summary>
        /// <param name="studentId" example="300000000000000000000003">The ID from the PmsFile's student to remove from the Database</param>
        /// <returns></returns>
        /// <response code="204">The PmsFile with the specified ID is removed from the Database - No content is returned</response>
        /// <response code="404">The PmsFile with the specified ID does not exist in the Database</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeletePmsFileAsync(string studentId)
        {
            var pmsFile = await pmsFileRepository.GetByIdAsync(studentId);
            if (pmsFile == null)
            {
                return NotFound($"Could not find PmsFile with id = {studentId}");
            }

            await pmsFileRepository.DeleteAsync(studentId);

            return NoContent();
        }
    }
}
