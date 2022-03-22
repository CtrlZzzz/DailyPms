using ClientServices.Interfaces;
using DailyPmsShared;
using System.Net.Http.Json;

namespace ClientServices
{
    public class HttpStudentService : IStudentService
    {
        private readonly HttpClient client;

        public HttpStudentService(HttpClient http)
        {
            client = http;
        }


        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var response = await client.GetAsync("/api/Students");

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Student>>();
            return result!;
        }

        public async Task<IEnumerable<Student>> GetStudentsBySchoolAsync(string schoolId)
        {
            var response = await client.GetAsync($"/api/Students/BySchool/{schoolId}");

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Student>>();
            return result!;
        }

        public async Task<Student> GetStudentById(string studentId)
        {
            var response = await client.GetAsync($"/api/Students/{studentId}");

            var result = await response.Content.ReadFromJsonAsync<Student>();
            return result!;
        }

    }
}
