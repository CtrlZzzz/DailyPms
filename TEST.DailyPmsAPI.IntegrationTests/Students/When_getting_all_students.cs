using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TEST.DailyPmsAPI.IntegrationTests.Students;

[Collection("Non-parallel test")]
public class When_getting_all_students : GetAllResourcesFixture<Student>
{
    public When_getting_all_students(WebApplicationFactory<StartupTest> factory)
        : base(factory)
    {
        //Arrange
        TestResources = TestItemsBuilder<Student>.BuildTestItems();
        MongoCollection?.InsertMany(TestResources);
    }


    [Fact]
    public async Task It_should_return_200_ok_all_students()
    {
        //Arrange
        //Act
        var response = await Act();
        response.EnsureSuccessStatusCode();
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task It_should_return_all_students_count()
    {
        //Arrange
        //Act
        var response = await Act();
        response.EnsureSuccessStatusCode();
        //Assert
        var result = await response.Content.ReadFromJsonAsync<List<Student>>();
        Assert.Equal(TestResources?.Count, result?.Count);
    }

    [Fact]
    public async Task It_should_return_the_correct_students()
    {
        //Arrange
        //Act
        var response = await Act();
        response.EnsureSuccessStatusCode();
        //Assert
        var apiResult = await response.Content.ReadFromJsonAsync<List<Student>>();
        foreach (var test in TestResources!)
        {
            var currentApiStudent = apiResult?.FirstOrDefault(s => s._id == test._id);

            Assert.NotNull(currentApiStudent);
            //Convert UTC datetime to Local datetime:
            var initialDate = currentApiStudent!.BirthDate;
            var LocalTimeDate = initialDate.ToLocalTime();
            currentApiStudent.BirthDate = LocalTimeDate;
            initialDate = currentApiStudent.RegistrationDate;
            LocalTimeDate = initialDate.ToLocalTime();
            currentApiStudent.RegistrationDate = LocalTimeDate;

            Assert.Equal(test.FirstName, currentApiStudent?.FirstName);
            Assert.Equal(test.LastName, currentApiStudent?.LastName);
            Assert.Equal(test.BirthDate, currentApiStudent?.BirthDate);
            Assert.Equal(test.Street, currentApiStudent?.Street);
            Assert.Equal(test.PostalCode, currentApiStudent?.PostalCode);
            Assert.Equal(test.City, currentApiStudent?.City);
            Assert.Equal(test.Phone, currentApiStudent?.Phone);
            Assert.Equal(test.Email, currentApiStudent?.Email);
            Assert.Equal(test.Parent1, currentApiStudent?.Parent1);
            Assert.Equal(test.Parent2, currentApiStudent?.Parent2);
            Assert.Equal(test.RegistrationDate, currentApiStudent?.RegistrationDate);
            Assert.Equal(test.SchoolID, currentApiStudent?.SchoolID);
            Assert.Equal(test.ClasseID, currentApiStudent?.ClasseID);
            Assert.Equal(test.PmsFileID, currentApiStudent?.PmsFileID);
        }
    }
}
