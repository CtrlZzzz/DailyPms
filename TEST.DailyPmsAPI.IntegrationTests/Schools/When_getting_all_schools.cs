using System.Threading.Tasks;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Linq;

namespace TEST.DailyPmsAPI.IntegrationTests.Schools
{
    public class When_getting_all_schools : WhenGettingAllItems<School>
    {
        public When_getting_all_schools(WebApplicationFactory<StartupTest> factory)
            : base(factory)
        {
        }

        public override async Task It_should_return_all_items()
        {
            // Arrange
            //Act
            var response = await testingClient.GetAsync(apiPath);
            response.EnsureSuccessStatusCode();
            //Assert
            var result = await response.Content.ReadFromJsonAsync<List<Student>>();
            Assert.Equal(8, result?.Count);
        }

        public override async Task It_should_return_the_correct_items()
        {
            //Arrange
            var TestSchools = BuildTestItems();
            //Act
            var response = await testingClient.GetAsync(apiPath);
            response.EnsureSuccessStatusCode();
            //Assert
            var apiResult = await response.Content.ReadFromJsonAsync<List<School>>();
            foreach (var test in TestSchools!)
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

