using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;
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
            return await dbContext.Schools.Find(school => school._id == id).FirstOrDefaultAsync();
        }

        public async Task<School> GetSchoolByNameAsync(string name)
        {
            //var result = await dbContext.Schools.Find(school => school.Name.ToLower().Contains(name) || school.Moniker.ToLower().Contains(name)).ToListAsync();
            return await dbContext.Schools.Find(school => school.Name == name || school.Moniker == name).FirstOrDefaultAsync();
        }

        public async Task UpdateSchoolByIdAsync(string id, School updatedSchool)
        {
            await dbContext.Schools.ReplaceOneAsync(school => school._id == id, updatedSchool);
        }

        public async Task CreateSchoolAsync(School newSchool)
        {
            if (newSchool == null)
            {
                throw new ArgumentNullException(nameof(newSchool));
            }

            await dbContext.Schools.InsertOneAsync(newSchool);
        }

        public async Task<bool> DeleteSchoolByIdAsync(string id)
        {
            var result = await dbContext.Schools.DeleteOneAsync(school => school._id == id);
            return result.DeletedCount > 0;
        }
    }
}
