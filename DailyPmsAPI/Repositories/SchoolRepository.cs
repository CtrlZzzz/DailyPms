using DailyPmsAPI.Data;
using DailyPmsShared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPmsAPI.Repositories
{
    public class SchoolRepository : MongoRepository<School>, IGetByName<School>
    {
        public SchoolRepository(IDatabase db)
            : base(db, "Schools") { }

        public async Task<IEnumerable<School>> GetByNameAsync(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var result = await Collection.FindAsync(s => s.Name.ToLower().Contains(name.ToLower()) || s.Moniker.ToLower().Contains(name.ToLower()));
            return await result.ToListAsync();
        }
    }
}
