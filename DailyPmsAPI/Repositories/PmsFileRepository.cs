using DailyPmsAPI.Data;
using DailyPmsShared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPmsAPI.Repositories
{
    public class PmsFileRepository : MongoRepository<PmsFile>
    {
        public PmsFileRepository(IDatabase db, string collectionName) 
            : base(db, "PmsFiles") {}

        public override Task<IEnumerable<PmsFile>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
