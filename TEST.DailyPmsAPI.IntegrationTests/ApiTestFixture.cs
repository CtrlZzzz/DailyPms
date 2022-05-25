using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace TEST.DailyPmsAPI.IntegrationTests
{
    public abstract class ApiTestFixture<T> : IntegrationTestFixture<T> where T : class, IEntity
    {
        private readonly string baseUri;

        public ApiTestFixture(WebApplicationFactory<StartupTest> factory, IConfiguration configuration, string? baseUri = null)
            : base(factory, configuration)
        {
            this.baseUri = baseUri ?? "/api/" + CollectionName + "/";
        }

        protected T? TestRessource { get; set; }


        public async Task<HttpResponseMessage> GetAsync(T resource, string? routeUri = null)
        {
            string requestUri = routeUri != null ? baseUri + routeUri + resource._id : baseUri + resource._id;

            return await testingClient.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsync(T resource)
        {
            return await testingClient.PostAsJsonAsync(baseUri, resource);
        }

        public async Task<HttpResponseMessage> PutAsync(T resource, string id)
        {
            return await testingClient.PutAsJsonAsync(baseUri + id, resource);
        }


    }
}

