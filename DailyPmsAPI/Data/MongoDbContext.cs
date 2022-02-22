using MongoDB.Driver;
using DailyPmsShared;

namespace DailyPmsAPI.Data
{
    public class MongoDbContext : IDbContext
    {
        readonly IMongoDatabase pmsDb;


        public MongoDbContext(IMongoClient dbClient)
        {
            pmsDb = dbClient.GetDatabase("PmsDb");
        }


        // review: Please use constants for hardcoded strings
        public IMongoCollection<School> Schools { get { return pmsDb.GetCollection<School>("Schools"); } }
        public IMongoCollection<Classe> Classes { get { return pmsDb.GetCollection<Classe>("Classes"); } }
        public IMongoCollection<Student> Students { get { return pmsDb.GetCollection<Student>("Students"); } }
        public IMongoCollection<PmsFile> PmsFiles { get { return pmsDb.GetCollection<PmsFile>("PmsFiles"); } }
        public IMongoCollection<PmsCenter> PmsCenters { get { return pmsDb.GetCollection<PmsCenter>("PmsCenters"); } }
        public IMongoCollection<Agent> Agents { get { return pmsDb.GetCollection<Agent>("Agents"); } }
    }
}
