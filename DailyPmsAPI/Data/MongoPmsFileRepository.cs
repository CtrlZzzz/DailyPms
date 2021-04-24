using System;
using System.Threading.Tasks;
using DailyPmsAPI.Models;
using MongoDB.Driver;

namespace DailyPmsAPI.Data
{
    public class MongoPmsFileRepository : IPmsFileRepository
    {
        readonly IDbContext dbContext;

        public MongoPmsFileRepository(IDbContext databaseContext)
        {
            dbContext = databaseContext;
        }


        public async Task<PmsFile> GetPmsFileByIdAsync(string id)
        {
            return await dbContext.PmsFiles.Find(file => file.PmsFileID == id).FirstOrDefaultAsync();
        }

        public async Task<PmsFile> GetPmsFileByStudentIdAsync(string studentId)
        {
            return await dbContext.PmsFiles.Find(file => file.StudentID == studentId).FirstOrDefaultAsync();
        }

        public async Task CreatePmsFileAsync(PmsFile newPmsFile)
        {
            if (newPmsFile == null)
            {
                throw new ArgumentNullException(nameof(newPmsFile));
            }

            await dbContext.PmsFiles.InsertOneAsync(newPmsFile);
        }

        public async Task UpDatePmsFileAsync(string id, PmsFile updatedPmsFile)
        {
            if (updatedPmsFile == null)
            {
                throw new ArgumentNullException(nameof(updatedPmsFile));
            }

            await dbContext.PmsFiles.ReplaceOneAsync(file => file.PmsFileID == id, updatedPmsFile);
        }

        public async Task DeletePmsFileAsync(string id)
        {
            await dbContext.PmsFiles.DeleteOneAsync(file => file.PmsFileID == id);
        }
    }
}
