using MongoDB.Driver;

namespace DailyPmsAPI.Data
{
    public class MongoTestDatabase : IDatabase
    {
        public MongoTestDatabase(IMongoClient dbClient)
        {
            PmsDb = dbClient.GetDatabase("PmsDbTest");
        }

        public IMongoDatabase PmsDb { get; set; }
    }
}

