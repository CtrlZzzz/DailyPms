using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DailyPmsAPI;
using DailyPmsShared;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TEST.DailyPmsAPI.IntegrationTests.Students
{
    public class When_getting_one_student : IClassFixture<WebApplicationFactory<Startup>>
    {
        readonly HttpClient apiClient;

        public When_getting_one_student(WebApplicationFactory<Startup> factory)
        {
            apiClient = factory.CreateClient();
            testStudent = BuildTestStudent();
            testStudentId = testStudent._id;
        }

        Student testStudent;
        string testStudentId;

        public Student BuildTestStudent()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Students/Test_Students.json");
            var jsonFile = File.ReadAllText(filePath);

            var StudentsFromJson = JsonSerializer.Deserialize<List<Student>>(jsonFile);
            return StudentsFromJson![1];
        }


        [Fact]
        public async Task It_should_return_200_ok()
        {
            var response = await apiClient.GetAsync("/api/students/" + testStudentId);
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task It_should_return_one_student()
        {
            var response = await apiClient.GetAsync("/api/students/" + testStudentId);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Student>();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task It_should_return_the_correct_student()
        {
            var response = await apiClient.GetAsync("/api/students/" + testStudentId);
            response.EnsureSuccessStatusCode();

            var apiStudent = await response.Content.ReadFromJsonAsync<Student>();
            Assert.Equal(testStudent.FirstName, apiStudent?.FirstName);
            Assert.Equal(testStudent.LastName, apiStudent?.LastName);
            Assert.Equal(testStudent.BirthDate, apiStudent?.BirthDate);
            Assert.Equal(testStudent.Street, apiStudent?.Street);
            Assert.Equal(testStudent.PostalCode, apiStudent?.PostalCode);
            Assert.Equal(testStudent.City, apiStudent?.City);
            Assert.Equal(testStudent.Phone, apiStudent?.Phone);
            Assert.Equal(testStudent.Email, apiStudent?.Email);
            Assert.Equal(testStudent.Parent1, apiStudent?.Parent1);
            Assert.Equal(testStudent.Parent2, apiStudent?.Parent2);
            Assert.Equal(testStudent.RegistrationDate, apiStudent?.RegistrationDate);
            Assert.Equal(testStudent.SchoolID, apiStudent?.SchoolID);
            Assert.Equal(testStudent.ClasseID, apiStudent?.ClasseID);
            Assert.Equal(testStudent.PmsFileID, apiStudent?.PmsFileID);
        }
    }
}

