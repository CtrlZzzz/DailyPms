using DailyPmsAPI.Data;
using DailyPmsShared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPmsAPI.Repositories
{
    public class AgentRepository : MongoRepository<Agent>, IGetByName<Agent>
    {
        public AgentRepository(IDatabase db, string collectionName) 
            : base(db, "Agents") {}

        public async Task<IEnumerable<Agent>> GetByNameAsync(string name)
        {
            if (name == null) 
                throw new ArgumentNullException("name");

            var result = await Collection.FindAsync(a => a.LastName.ToLower().Contains(name.ToLower()));
            return await result.ToListAsync();
        }
    }
}
