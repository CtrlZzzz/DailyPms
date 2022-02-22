using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsClient.Services
{
    public class StudentDataService : IStudentDataService
    {
        readonly HttpClient httpClient;

        public StudentDataService(HttpClient client)
        {
            httpClient = client;
        }


        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var response = await httpClient.GetStreamAsync("api/Students");
            // review: do we let any error bubble up to the viewmodels ?
            return await JsonSerializer.DeserializeAsync<IEnumerable<Student>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
        }

        public async Task<Student> GetStudentByIdAsync(string id)
        {
            var response = await httpClient.GetStreamAsync($"api/Students/{id}");
            return await JsonSerializer.DeserializeAsync<Student>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public Task CreateStudentAsync(Student newStudent)
        {
            // review: no validation ?
            throw new NotImplementedException();
        }

        public Task UpdateStudentByIdAsync(string id, Student updatedStudent)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudentByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
