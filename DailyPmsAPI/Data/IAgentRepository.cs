using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsAPI.Data
{
    // review: All the repository interfaces are very repetitive code.
    // Is it possible to write some base interface with generic signature for all repositories ?
    public interface IAgentRepository
    {
        Task<IEnumerable<Agent>> GetAllAgentByCenterAsync(string centerId);

        Task<Agent> GetAgentByIdAsync(string id);

        Task<Agent> GetAgentByNameAsync(string name, string centerId);

        Task CreateAgentAsync(Agent newAgent);

        Task UpdateAgentAsync(string id, Agent updatedAgent);

        Task DeleteAgentAsync(string id);
    }
}
