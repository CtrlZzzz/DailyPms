using DailyPmsShared;

namespace ClientServices.Interfaces
{
    public interface IAgentService
    {
        Task<IEnumerable<Agent>> GetAgentByNameAsync(string name);
    }
}

