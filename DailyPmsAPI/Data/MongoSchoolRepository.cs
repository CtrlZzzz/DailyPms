using System;
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

        
    }
}
