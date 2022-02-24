using ClientServices.Interfaces;
using DailyPmsShared;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

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
        public async Task<School> GetSchoolByIdAsync(string schoolId)
        {
            var response = await client.GetAsync($"/api/Schools/{schoolId}");

            var result = await response.Content.ReadFromJsonAsync<School>();
            return result;
        }

        public async Task EditSchoolByIdAsync(string id, School editedSchool)
        {
            //var serializedSchool = JsonSerializer.Serialize(editedSchool);
            //var requestContent = new StringContent(serializedSchool, Encoding.UTF8, "application/json");
            //var response = await client.PutAsync($"/api/Schools/{id}", requestContent);
            await client.PutAsJsonAsync($"/api/Schools/{id}", editedSchool);
        }
    }
}

