using DailyPmsAPI.Data;
using DailyPmsShared;

namespace DailyPmsAPI.Repositories
{
    public class PmsCenterRepository : MongoRepository<PmsCenter>
    {
        public PmsCenterRepository(IDatabase db, string collectionName) 
            : base(db, "PmsCenters") {}
    }
}
