using System;
using System.Net.Http;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Xunit;

namespace TEST.DailyPmsAPI.IntegrationTests
{
    public abstract class IntegrationTestFixture<T> : IDisposable, IClassFixture<WebApplicationFactory<StartupTest>> where T : class, IEntity
    {
        const string DBNAME = "PmsDbTest";

        protected readonly HttpClient testingClient;
        protected readonly IConfiguration configuration;

        protected IntegrationTestFixture(WebApplicationFactory<StartupTest> factory)
        {
            var testingClientOptions = new WebApplicationFactoryClientOptions();
            //testingClientOptions.BaseAddress = new Uri("https://dailypmsapi.azurewebsites.net");
            testingClientOptions.BaseAddress = new Uri("https://localhost:5001/");
            testingClient = factory.CreateClient(testingClientOptions) ?? throw new NullReferenceException(nameof(testingClient));

            this.configuration = new ConfigurationBuilder()
                .AddUserSecrets<IntegrationTestFixture<T>>()
                .Build();

            ConfigureMongo();
        }

        public string? CollectionName { get; private set; }
        public IMongoDatabase? MongoDb { get; private set; }
        public IMongoCollection<T>? MongoCollection { get; private set; }


        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);

            testingClient?.Dispose();
            MongoDb?.DropCollection(CollectionName);
        }

        protected virtual string GetUserSecret(string secretName)
        {
            //var test = configuration["ConnectionStrings:MongoConnection"];
            return configuration.GetConnectionString(secretName);
        }

        protected virtual void ConfigureMongo()
        {
            var MongoConnectionString = GetUserSecret("MongoConnection") ?? throw new NullReferenceException("Can not retreive the Mongo connection string from user-secrets !");
            var settings = MongoClientSettings.FromConnectionString(MongoConnectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var mongoClient = new MongoClient(settings);
            MongoDb = mongoClient.GetDatabase(DBNAME) ?? throw new NullReferenceException(nameof(MongoDb));
            CollectionName = typeof(T).Name + "s";
            MongoCollection = MongoDb.GetCollection<T>(CollectionName) ?? throw new NullReferenceException(nameof(MongoCollection));
        }
    }
}

