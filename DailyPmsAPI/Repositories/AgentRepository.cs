using DailyPmsAPI.Data;
using DailyPmsShared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPmsAPI.Repositories
{
    public class AgentRepository : MongoRepository<Agent>, IGetByName<Agent>, IGetAllFromId<Agent>
    {
        public AgentRepository(IDatabase db)
            : base(db, "Agents") { }

        public async Task<IEnumerable<Agent>> GetAllFromIdAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var result = await Collection.FindAsync(a => a.CenterID == id);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Agent>> GetByNameAsync(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var result = await Collection.FindAsync(a => a.Surname.ToLower().Contains(name.ToLower()));
            return await result.ToListAsync();
        }
    }
}
