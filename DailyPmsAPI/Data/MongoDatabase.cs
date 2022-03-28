using MongoDB.Driver;

namespace DailyPmsAPI.Data
{
    public class MongoDatabase : IDatabase
    {
        public MongoDatabase(IMongoClient dbClient)
        {
            PmsDb = dbClient.GetDatabase("PmsDb");
        }

        public IMongoDatabase PmsDb { get; set; }
    }
}
