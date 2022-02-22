using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;
using MongoDB.Driver;

namespace DailyPmsAPI.Data
{
    // review: All the repositories are very repetitive code.
    // Is it possible to write some base class with generic code for all repositories ?
    public class MongoAgentRepository : IAgentRepository
    {
        readonly IDbContext dbContext;

        public MongoAgentRepository(IDbContext databaseContext)
        {
            // review: Please use defensive programming, check all method arguments
            // Can databaseContext be null ?
            dbContext = databaseContext;
        }

        public async Task<IEnumerable<Agent>> GetAllAgentByCenterAsync(string centerId)
        {
            // review: Please use defensive programming, check all method arguments
            return await dbContext.Agents.Find(agent => agent.CenterID == centerId).ToListAsync();
        }

        public async Task<Agent> GetAgentByIdAsync(string id)
        {
            // review: Please use defensive programming, check all method arguments
            return await dbContext.Agents.Find(agent => agent.AgentId == id).FirstOrDefaultAsync();
        }

        public async Task<Agent> GetAgentByNameAsync(string name, string centerId)
        {
            // review: Please use defensive programming, check all method arguments
            return await dbContext.Agents.Find(agent => agent.LastName == name && agent.CenterID == centerId).FirstOrDefaultAsync();
        }

        public async Task CreateAgentAsync(Agent newAgent)
        {
            if (newAgent == null)
            {
                throw new ArgumentNullException(nameof(newAgent));
            }

            await dbContext.Agents.InsertOneAsync(newAgent);
        }

        public async Task UpdateAgentAsync(string id, Agent updatedAgent)
        {
            if (updatedAgent == null)
            {
                throw new ArgumentNullException(nameof(updatedAgent));
            }

            await dbContext.Agents.ReplaceOneAsync(agent => agent.AgentId == id, updatedAgent);
        }

        public async Task DeleteAgentAsync(string id)
        {
            await dbContext.Agents.DeleteOneAsync(agent => agent.AgentId == id);
        }
    }
}
