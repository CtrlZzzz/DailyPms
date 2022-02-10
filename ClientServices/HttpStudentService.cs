using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var response = await client.GetAsync("/api/students");

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Student>>();
            return result;
        }
    }
}
