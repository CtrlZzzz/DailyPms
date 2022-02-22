using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;
using MongoDB.Driver;

namespace DailyPmsAPI.Data
{
    public class MongoPmsCenterRepository : IPmsCenterRepository
    {
        readonly IDbContext dbContext;

        public MongoPmsCenterRepository(IDbContext databaseContext)
        {
            dbContext = databaseContext;
        }


        public async Task<IEnumerable<PmsCenter>> GetAllCentersAsync()
        {
            // // review: isn't there a FindAll method ? (or similar) .. this will generate a query with an unnecessary filter
            return await dbContext.PmsCenters.Find(center => true).ToListAsync();
        }

        public async Task<PmsCenter> GetCenterByIdAsync(string id)
        {
            return await dbContext.PmsCenters.Find(center => center.CenterID == id).FirstOrDefaultAsync();
        }

        public async Task<PmsCenter> GetCenterByNameAsync(string name)
        {
            return await dbContext.PmsCenters.Find(center => center.Name == name).FirstOrDefaultAsync();
        }

        public async Task UpdateCenterByIdAsync(string id, PmsCenter updatedCenter)
        {
            await dbContext.PmsCenters.ReplaceOneAsync(center => center.CenterID == id, updatedCenter);
        }

        public async Task CreateCenterAsync(PmsCenter newCenter)
        {
            if (newCenter == null)
            {
                throw new ArgumentNullException(nameof(newCenter));
            }

            await dbContext.PmsCenters.InsertOneAsync(newCenter);
        }

        public async Task DeleteCenterByIdAsync(string id)
        {
            await dbContext.PmsCenters.DeleteOneAsync(center => center.CenterID == id);
        }
    }
}
