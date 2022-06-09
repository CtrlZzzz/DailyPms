using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Data;
using DailyPmsShared;
using MongoDB.Driver;

namespace DailyPmsAPI.Repositories
{
    public class PmsCenterRepository : MongoRepository<PmsCenter>, IGetByName<PmsCenter>
    {
        public PmsCenterRepository(IDatabase db)
            : base(db, "PmsCenters") { }

        public async Task<IEnumerable<PmsCenter>> GetByNameAsync(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            IAsyncCursor<PmsCenter> result = await Collection.FindAsync(s => s.Name.ToLower().Contains(name.ToLower()));
            return await result.ToListAsync();
        }
    }
}
