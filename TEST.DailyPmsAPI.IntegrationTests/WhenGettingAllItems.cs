using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TEST.DailyPmsAPI.IntegrationTests
{
    public abstract class WhenGettingAllItems<T> : IClassFixture<WebApplicationFactory<Startup>>, ITestWhenGettingAll<T> where T : class, IEntity
    {
        protected readonly HttpClient testingClient;
        protected readonly string apiPath;

        public WhenGettingAllItems(WebApplicationFactory<Startup> factory)
        {
            testingClient = factory.CreateClient();
            apiPath = "/api/" + typeof(T).Name + "s";
        }

        public virtual IList<T> BuildTestItems()
        {


            var typeName = typeof(T).Name;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), typeName + "s/Test_" + typeName + "s.json");

            var jsonFile = File.ReadAllText(filePath);
            var itemsFromJson = JsonSerializer.Deserialize<IList<T>>(jsonFile);

            return itemsFromJson!;
        }

        [Fact]
        public virtual async Task It_should_return_200_ok()
        {
            //Arrange
            //Act
            var response = await testingClient.GetAsync(apiPath);
            response.EnsureSuccessStatusCode();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public abstract Task It_should_return_all_items();

        [Fact]
        public abstract Task It_should_return_the_correct_items();
    }
}

