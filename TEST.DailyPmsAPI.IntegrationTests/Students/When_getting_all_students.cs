using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace TEST.DailyPmsAPI.IntegrationTests;

public class When_getting_all_students : IClassFixture<WebApplicationFactory<StartupTest>>
{
    readonly HttpClient apiClient;
    private readonly IConfiguration config;

    public When_getting_all_students(WebApplicationFactory<StartupTest> factory)
    {
        apiClient = factory.CreateClient();
        this.config = new ConfigurationBuilder()
            .AddUserSecrets<When_getting_all_students>()
            .Build();
    }


    public List<Student>? BuildTestStudents()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Students/Test_Students.json");
        var jsonFile = File.ReadAllText(filePath);

        var StudentsFromJson = JsonSerializer.Deserialize<List<Student>>(jsonFile);

        return StudentsFromJson;
    }


    [Fact]
    public async Task It_should_return_200_ok_all_students()
    {
        //test
        var test = config["ConnectionStrings:MongoConnection"];
        var test2 = config.GetConnectionString("MongoConnection");
        //Arrange
        //Act
        var response = await apiClient.GetAsync("/api/Students");
        response.EnsureSuccessStatusCode();
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task It_should_return_all_students_count()
    {
        //Arrange
        //Act
        var response = await apiClient.GetAsync("/api/Students");
        response.EnsureSuccessStatusCode();
        //Assert
        var result = await response.Content.ReadFromJsonAsync<List<Student>>();
        Assert.Equal(240, result?.Count);
    }

    [Fact]
    public async Task It_should_return_the_correct_students()
    {
        //Arrange
        var TestStudents = BuildTestStudents();
        //Act
        var response = await apiClient.GetAsync("/api/Students");
        response.EnsureSuccessStatusCode();
        //Assert
        var apiResult = await response.Content.ReadFromJsonAsync<List<Student>>();
        foreach (var test in TestStudents!)
        {
            var currentApiStudent = apiResult?.FirstOrDefault(s => s._id == test._id);
            Assert.NotNull(currentApiStudent);
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
