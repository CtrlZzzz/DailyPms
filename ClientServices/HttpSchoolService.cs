using ClientServices.Interfaces;
using DailyPmsShared;
using System.Net.Http.Json;

namespace ClientServices
{
    public class HttpSchoolService : ISchoolService
	{
        private readonly HttpClient client;

		public HttpSchoolService(HttpClient http)
		{
            client = http;
		}

        public async Task<IEnumerable<School>> GetSchoolSummariesAsync()
        {
            var response = await client.GetAsync("/api/Schools");

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<School>>();
            return result;
        }
    }
}

