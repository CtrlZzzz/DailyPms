using MongoDB.Driver;
using DailyPmsShared;

namespace DailyPmsAPI.Data
{
    public class MongoDbContext : IDbContext
    {
        //readonly IMongoDatabase pmsDb;
        //readonly IMongoClient dbClient;

        public MongoDbContext(IMongoClient dbClient)
        {
            //this.dbClient = dbClient;
            PmsDb = dbClient.GetDatabase("PmsDb");
        }

        public IMongoDatabase PmsDb { get; set; }
        //public IMongoDatabase PmsDb { get => dbClient.GetDatabase("PmsDb"); }

        public IMongoCollection<School> Schools { get { return PmsDb.GetCollection<School>("Schools"); } }
        public IMongoCollection<Class> Classes { get { return PmsDb.GetCollection<Class>("Classes"); } }
        public IMongoCollection<Student> Students { get { return PmsDb.GetCollection<Student>("Students"); } }
        public IMongoCollection<PmsFile> PmsFiles { get { return PmsDb.GetCollection<PmsFile>("PmsFiles"); } }
        public IMongoCollection<PmsCenter> PmsCenters { get { return PmsDb.GetCollection<PmsCenter>("PmsCenters"); } }
        public IMongoCollection<Agent> Agents { get { return PmsDb.GetCollection<Agent>("Agents"); } }
    }
}
