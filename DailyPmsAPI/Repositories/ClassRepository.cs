using DailyPmsAPI.Data;
using DailyPmsShared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPmsAPI.Repositories
{
    public class ClassRepository : MongoRepository<Class>, IGetAllFromId<Class>
    {
        public ClassRepository(IDatabase db, string collectionName) 
            : base(db, "Classes") {}

        public async Task<IEnumerable<Class>> GetAllFromIdAsync(string id)
        {
            if(id == null)
                throw new ArgumentNullException("id");

            var result = await Collection.FindAsync(c => c.SchoolID == id);
            return await result.ToListAsync();
        }
    }
}
