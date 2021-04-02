using System.Collections.Generic;
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

        public IEnumerable<School> GetAllSchools()
        {
            return dbContext.Schools.Find(school => true).ToList();
        }

        public School GetSchoolById(string id)
        {
            return dbContext.Schools.Find(school => school.SchoolID == id).FirstOrDefault();
        }

        //public void UpdateSchool(School schoolToUpdate, School updatedSchool)
        //{
        //    //TODO
        //}

        public void UpdateSchoolById(string id, School updatedSchool)
        {
            dbContext.Schools.ReplaceOne(school => school.SchoolID == id, updatedSchool);
        }
    }
}
