using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Models;
using MongoDB.Driver;

namespace DailyPmsAPI.Data
{
    public class MongoSchoolRepository : ISchoolRepository
    {
        readonly IDbContext dbContext;

        public MongoSchoolRepository(IDbContext databaseContext)
        {
            dbContext = databaseContext;
        }


        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await dbContext.Schools.Find(school => true).ToListAsync();
        }

        public async Task<School> GetSchoolByIdAsync(string id)
        {
            return await dbContext.Schools.Find(school => school.SchoolID == id).FirstOrDefaultAsync();
        }

        public async Task<School> GetSchoolByNameAsync(string name)
        {
            return await dbContext.Schools.Find(school => school.Name == name || school.Moniker == name).FirstOrDefaultAsync();
        }

        public async Task UpdateSchoolByIdAsync(string id, School updatedSchool)
        {
            await dbContext.Schools.ReplaceOneAsync(school => school.SchoolID == id, updatedSchool);
        }
    }
}
