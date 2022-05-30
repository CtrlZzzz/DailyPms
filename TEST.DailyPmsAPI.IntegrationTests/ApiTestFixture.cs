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

        public async Task<HttpResponseMessage> GetAllAsync()
        {
            return await testingClient.GetAsync(baseUri);
        }

        public async Task<HttpResponseMessage> PostAsync(T resource)
        {
            return await testingClient.PostAsJsonAsync(baseUri, resource);
        }

        public async Task<HttpResponseMessage> PutAsync(T resource, string id)
        {
            return await testingClient.PutAsJsonAsync(baseUri + id, resource);
        }

        public async Task<HttpResponseMessage> DeleteAsync(T resource)
        {
            return await testingClient.DeleteAsync(baseUri + resource._id);
        }
    }




    public abstract class GetResourceFixture<T> : ApiTestFixture<T> where T : class, IEntity
    {
        protected GetResourceFixture(WebApplicationFactory<StartupTest> factory, IConfiguration configuration, string? baseUri = null)
            : base(factory, configuration, baseUri)
        {
        }

        protected async Task<HttpResponseMessage> Act(string? routeUri = null)
        {
            if (TestRessource == null) throw new ArgumentNullException(nameof(TestRessource));

            return await GetAsync(TestRessource, routeUri);
        }
    }


    public abstract class GetAllResourcesFixture<T> : ApiTestFixture<T> where T : class, IEntity
    {
        protected GetAllResourcesFixture(WebApplicationFactory<StartupTest> factory, IConfiguration configuration, string? baseUri = null)
            : base(factory, configuration, baseUri)
        {
        }

        protected async Task<HttpResponseMessage> Act()
        {
            return await GetAllAsync();
        }
    }


    public abstract class PostResourceFixture<T> : ApiTestFixture<T> where T : class, IEntity
    {
        protected PostResourceFixture(WebApplicationFactory<StartupTest> factory, IConfiguration configuration, string? baseUri = null) 
            : base(factory, configuration, baseUri)
        {
        }

        protected async Task<HttpResponseMessage> Act()
        {
            if (TestRessource == null) throw new ArgumentNullException(nameof(TestRessource));

            return await PostAsync(TestRessource);
        }
    }


    public abstract class PutResourceFixture<T> : ApiTestFixture<T> where T : class, IEntity
    {
        protected PutResourceFixture(WebApplicationFactory<StartupTest> factory, IConfiguration configuration, string? baseUri = null)
            : base(factory, configuration, baseUri)
        {
        }

        protected async Task<HttpResponseMessage> Act(string Id)
        {
            if (TestRessource == null) throw new ArgumentNullException(nameof(TestRessource));
            if (Id == null) throw new ArgumentNullException(nameof(Id));
            
            return await PutAsync(TestRessource, Id);
        }
    }


    public abstract class DeleteResourceFixture<T> : ApiTestFixture<T> where T : class, IEntity
    {
        protected DeleteResourceFixture(WebApplicationFactory<StartupTest> factory, IConfiguration configuration, string? baseUri = null)
            : base(factory, configuration, baseUri)
        {
        }

        protected async Task<HttpResponseMessage> Act()
        {
            if (TestRessource == null) throw new ArgumentNullException(nameof(TestRessource));

            return await DeleteAsync(TestRessource);
        }
    }
}

