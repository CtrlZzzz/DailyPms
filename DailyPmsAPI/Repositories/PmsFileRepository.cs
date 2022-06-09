using DailyPmsAPI.Data;
using DailyPmsShared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPmsAPI.Repositories
{
    public class PmsFileRepository : MongoRepository<PmsFile>
    {
        public PmsFileRepository(IDatabase db)
            : base(db, "PmsFiles") { }

        public override Task<IEnumerable<PmsFile>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<PmsFile> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            IAsyncCursor<PmsFile> result = await Collection.FindAsync(i => i.StudentID == id);
            return await result.SingleOrDefaultAsync();
        }
    }
}
