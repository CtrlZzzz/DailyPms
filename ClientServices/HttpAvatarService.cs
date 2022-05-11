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

        public async Task<Stream?> GetImageStreamAsync(string studentId)
        {
            HttpResponseMessage blobResponse;

            var pictureResponse = await client.GetAsync($"/api/Pictures/{studentId}");
            if (pictureResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                var studentPictureContent = await pictureResponse.Content.ReadFromJsonAsync<StudentPicture>();
                blobResponse = await client.GetAsync($"/api/blob/{studentPictureContent?.BlobName}");
                var avatar = await blobResponse.Content.ReadAsStreamAsync();

                return avatar;
            }
        }

        public async Task<string?> GetImageUriAsync(string studentId)
        {
            var pictureResponse = await client.GetAsync($"/api/Pictures/{studentId}");
            if (pictureResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            var studentPictureContent = await pictureResponse.Content.ReadFromJsonAsync<StudentPicture>();
            var uriResponse = await client.GetAsync($"/api/blob/Uri/{studentPictureContent?.BlobName}");
            var uriString = await uriResponse.Content.ReadFromJsonAsync<string>();

            return uriString;
        }
    }
}
