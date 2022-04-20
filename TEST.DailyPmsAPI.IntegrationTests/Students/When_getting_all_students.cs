using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TEST.DailyPmsAPI.IntegrationTests;

public class When_getting_all_students : IClassFixture<WebApplicationFactory<Startup>>
{
    readonly HttpClient httpClient;

    public When_getting_all_students(WebApplicationFactory<Startup> factory)
    {
        httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task It_should_return_200_ok_all_students()
    {
        //Arrange
        //Act
        var response = await httpClient.GetAsync("/api/Students");
        response.EnsureSuccessStatusCode();
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task It_should_return_all_students_count()
    {
        //Arrange
        //Act
        var response = await httpClient.GetAsync("/api/Students");
        response.EnsureSuccessStatusCode();
        //Assert
        var result = await response.Content.ReadFromJsonAsync<List<Student>>();
        Assert.Equal(240, result?.Count);
    }
}
