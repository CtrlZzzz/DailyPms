using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TEST.DailyPmsAPI.IntegrationTests.Schools
{
    [Collection("Non-parallel test")]
    public class When_getting_one_school : GetResourceFixture<School>
    {
        public When_getting_one_school(WebApplicationFactory<StartupTest> factory, string? baseUri = null) 
            : base(factory, baseUri)
        {
            TestResources = TestItemsBuilder<School>.BuildTestItems();
            MongoCollection?.InsertMany(TestResources);
            TestResource = TestResources[1];        
        }


        [Fact]
        public async Task It_should_return_200_ok()
        {
            var response = await Act();
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task It_should_return_one_school()
        {
            var response = await Act();
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<School>();
            Assert.NotNull(result); 
        }

        [Fact]
        public async Task It_should_return_the_correct_school()
        {
            var response = await Act();
            response.EnsureSuccessStatusCode();
            var apiSchool = await response.Content.ReadFromJsonAsync<School>();

            Assert.Equal(TestResource?.Name, apiSchool?.Name);
            Assert.Equal(TestResource?.Moniker, apiSchool?.Moniker);
            Assert.Equal(TestResource?.Street, apiSchool?.Street);
            Assert.Equal(TestResource?.PostalCode, apiSchool?.PostalCode);
            Assert.Equal(TestResource?.City, apiSchool?.City);
            Assert.Equal(TestResource?.Phone, apiSchool?.Phone);
            Assert.Equal(TestResource?.Email, apiSchool?.Email);
            Assert.Equal(TestResource?.DirectorName, apiSchool?.DirectorName);
            Assert.Equal(TestResource?.PmsCenterID, apiSchool?.PmsCenterID);
            Assert.Equal(TestResource?.ClasseIDs, apiSchool?.ClasseIDs);
            Assert.Equal(TestResource?.StudentIDs, apiSchool?.StudentIDs);
        }
    }
}
