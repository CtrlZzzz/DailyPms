using DailyPmsAPI.Data;
using DailyPmsShared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPmsAPI.Repositories
{
    public class ClassRepository : MongoRepository<Class>, IGetAllFromId<Class>, IGetByName<Class>
    {
        public ClassRepository(IDatabase db)
            : base(db, "Classes") { }

        public async Task<IEnumerable<Class>> GetAllFromIdAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var result = await Collection.FindAsync(c => c.SchoolID == id);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetByNameAsync(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var result = await Collection.FindAsync(s => s.Name.ToLower().Contains(name.ToLower()));
            return await result.ToListAsync();
        }
    }
}
