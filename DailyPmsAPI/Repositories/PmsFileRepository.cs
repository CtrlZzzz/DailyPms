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
            : base(db, "PmsFiles") { }

        public override Task<IEnumerable<PmsFile>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        //public override async Task<PmsFile> GetByIdAsync(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //        throw new ArgumentNullException(nameof(id));

        //    var result = await Collection.FindAsync(i => i._id == id);
        //    return await result.SingleOrDefaultAsync();
        //}
    }
}
