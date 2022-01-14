using System;
using System.Net.Http.Json;
using ClientServices.Interfaces;
using DailyPmsShared.DTOs;

namespace ClientServices
{
	public class HttpSchoolService : ISchoolService
	{
        private readonly HttpClient client;

		public HttpSchoolService(HttpClient http)
		{
            client = http;
		}

        public async Task<IEnumerable<SchoolSummary>> GetSchoolSummariesAsync()
        {
            var response = await client.GetAsync("/api/Schools");

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<SchoolSummary>>();
            return result;
        }
    }
}

