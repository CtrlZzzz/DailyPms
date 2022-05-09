using ClientServices.Interfaces;
using System.Net.Http.Json;
using DailyPmsShared;

namespace ClientServices
{
    public class HttpAvatarService : IAvatarService
    {
        private readonly HttpClient client;

        public HttpAvatarService(HttpClient http)
        {
            client = http;
        }

        public async Task<Stream> GetStudentImageAsync(string studentId)
        {
            var pictureResponse = await client.GetAsync($"/api/Pictures/{studentId}");
            var studentPicture = await pictureResponse.Content.ReadFromJsonAsync<StudentPicture>();

            var blobResponse = await client.GetAsync($"/api/blob/{studentPicture?.BlobName}");
            var avatar = await blobResponse.Content.ReadAsStreamAsync();

            return avatar;
        }
    }
}
