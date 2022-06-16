using System.Net.Http.Json;
using ClientServices.Interfaces;
using DailyPmsShared;

namespace ClientServices
{
    public class HttpAgentService : IAgentService
    {
        private readonly HttpClient client;

        public HttpAgentService(HttpClient http)
        {
            client = http;
        }

        public async Task<IEnumerable<Agent>> GetAgentByNameAsync(string name)
        {
            var response = await client.GetAsync($"/api/Agents/ByName/{name}");

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Agent>>();
            return result!;
        }
    }
}

