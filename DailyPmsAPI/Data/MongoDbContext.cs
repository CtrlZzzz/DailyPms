using System;
using MongoDB.Driver;
using DailyPmsAPI.Models;

namespace DailyPmsAPI.Data
{
    public class MongoDbContext : IDbContext
    {
        readonly IMongoDatabase schoolDb;


        public MongoDbContext(IMongoClient dbClient)
        {
            schoolDb = dbClient.GetDatabase("PmsDb");
        }


        public IMongoCollection<School> Schools { get { return schoolDb.GetCollection<School>("Schools"); } }
    }
}
