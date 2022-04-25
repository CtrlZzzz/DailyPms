using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace TEST.DailyPmsAPI.IntegrationTests;

public class When_getting_all_students : IClassFixture<WebApplicationFactory<Startup>>
{
    readonly HttpClient apiClient;

    public When_getting_all_students(WebApplicationFactory<Startup> factory)
    {
        apiClient = factory.CreateClient();
        ExistingStudents = BuildExistingStudents();
    }

    public IList<Student>? ExistingStudents { get; set; }

    public IList<Student>? BuildExistingStudents()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Students/Test_Students.json");
        var jsonFile = File.ReadAllText(filePath);

        var StudentsFromJson = JsonConvert.DeserializeObject<IList<Student>>(jsonFile);

        return StudentsFromJson;
    }


    [Fact]
    public async Task It_should_return_200_ok_all_students()
    {
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
}
