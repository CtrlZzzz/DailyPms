using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsAPI.Data
{
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
