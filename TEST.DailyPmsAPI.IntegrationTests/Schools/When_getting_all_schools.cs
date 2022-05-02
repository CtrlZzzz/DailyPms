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
        public When_getting_all_schools(WebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        public override async Task It_should_return_all_items()
        {
            // Arrange
            //Act
            var response = await testingClient.GetAsync("/api/Schools");
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
            var response = await testingClient.GetAsync("/api/Schools");
            response.EnsureSuccessStatusCode();
            //Assert
            var apiResult = await response.Content.ReadFromJsonAsync<List<School>>();
            foreach (var test in TestSchools!)
            {
                var currentApiStudent = apiResult?.FirstOrDefault(s => s._id == test._id);
                Assert.NotNull(currentApiStudent);
                Assert.Equal(test.Name, currentApiStudent?.Name);
                Assert.Equal(test.Moniker, currentApiStudent?.Moniker);
                Assert.Equal(test.Street, currentApiStudent?.Street);
                Assert.Equal(test.PostalCode, currentApiStudent?.PostalCode);
                Assert.Equal(test.City, currentApiStudent?.City);
                Assert.Equal(test.Phone, currentApiStudent?.Phone);
                Assert.Equal(test.Email, currentApiStudent?.Email);
                Assert.Equal(test.DirectorName, currentApiStudent?.DirectorName);
                Assert.Equal(test.PmsCenterID, currentApiStudent?.PmsCenterID);
                Assert.Equal(test.ClasseIDs, currentApiStudent?.ClasseIDs);
                Assert.Equal(test.StudentIDs, currentApiStudent?.StudentIDs);
            }
        }
    }
}

