using System.Threading.Tasks;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TEST.DailyPmsAPI.IntegrationTests.Schools
{
    [Collection("Non-parallel test")]
    public class When_getting_all_schools : GetAllResourcesFixture<School>
    {
        public When_getting_all_schools(WebApplicationFactory<StartupTest> factory)
            : base(factory)
        {
            //Arrange
            TestResources = TestItemsBuilder<School>.BuildTestItems();
            MongoCollection?.InsertMany(TestResources);
        }

        [Fact]
        public async Task It_should_return_200_ok_all_schools()
        {
            //Act
            var response = await Act();
            response.EnsureSuccessStatusCode();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task It_should_return_all_schools_count()
        {
            //Act
            var response = await Act();
            response.EnsureSuccessStatusCode();
            //Assert
            var result = await response.Content.ReadFromJsonAsync<List<Student>>();
            Assert.Equal(TestResources?.Count, result?.Count);
        }

        [Fact]
        public async Task It_should_return_the_correct_items()
        {
            //Act
            var response = await Act();
            response.EnsureSuccessStatusCode();
            //Assert
            var apiResult = await response.Content.ReadFromJsonAsync<List<School>>();
            foreach (var test in TestResources!)
            {
                var currentApiSchool = apiResult?.FirstOrDefault(s => s._id == test._id);

                Assert.NotNull(currentApiSchool);
                Assert.Equal(test.Name, currentApiSchool?.Name);
                Assert.Equal(test.Moniker, currentApiSchool?.Moniker);
                Assert.Equal(test.Street, currentApiSchool?.Street);
                Assert.Equal(test.PostalCode, currentApiSchool?.PostalCode);
                Assert.Equal(test.City, currentApiSchool?.City);
                Assert.Equal(test.Phone, currentApiSchool?.Phone);
                Assert.Equal(test.Email, currentApiSchool?.Email);
                Assert.Equal(test.DirectorName, currentApiSchool?.DirectorName);
                Assert.Equal(test.PmsCenterID, currentApiSchool?.PmsCenterID);
                Assert.Equal(test.ClasseIDs, currentApiSchool?.ClasseIDs);
                Assert.Equal(test.StudentIDs, currentApiSchool?.StudentIDs);
            }
        }
    }
}

