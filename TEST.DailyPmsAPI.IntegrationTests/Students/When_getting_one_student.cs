using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TEST.DailyPmsAPI.IntegrationTests.Students
{
    [Collection("Non-parallel test")]
    public class When_getting_one_student : GetResourceFixture<Student>
    {
        public When_getting_one_student(WebApplicationFactory<StartupTest> factory)
            : base(factory)
        {
            TestResources = TestItemsBuilder<Student>.BuildTestItems();
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
        public async Task It_should_return_one_student()
        {
            var response = await Act();
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Student>();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task It_should_return_the_correct_student()
        {
            var response = await Act();
            response.EnsureSuccessStatusCode();
            var apiStudent = await response.Content.ReadFromJsonAsync<Student>();
            //Convert UTC datetime to Local datetime:
            var initialDate = apiStudent!.BirthDate;
            var LocalTimeDate = initialDate.ToLocalTime();
            apiStudent.BirthDate = LocalTimeDate;
            initialDate = apiStudent.RegistrationDate;
            LocalTimeDate = initialDate.ToLocalTime();
            apiStudent.RegistrationDate = LocalTimeDate;

            Assert.Equal(TestResource?.FirstName, apiStudent?.FirstName);
            Assert.Equal(TestResource?.LastName, apiStudent?.LastName);
            Assert.Equal(TestResource?.BirthDate, apiStudent?.BirthDate);
            Assert.Equal(TestResource?.Street, apiStudent?.Street);
            Assert.Equal(TestResource?.PostalCode, apiStudent?.PostalCode);
            Assert.Equal(TestResource?.City, apiStudent?.City);
            Assert.Equal(TestResource?.Phone, apiStudent?.Phone);
            Assert.Equal(TestResource?.Email, apiStudent?.Email);
            Assert.Equal(TestResource?.Parent1, apiStudent?.Parent1);
            Assert.Equal(TestResource?.Parent2, apiStudent?.Parent2);
            Assert.Equal(TestResource?.RegistrationDate, apiStudent?.RegistrationDate);
            Assert.Equal(TestResource?.SchoolID, apiStudent?.SchoolID);
            Assert.Equal(TestResource?.ClasseID, apiStudent?.ClasseID);
            Assert.Equal(TestResource?.PmsFileID, apiStudent?.PmsFileID);
        }
    }
}

