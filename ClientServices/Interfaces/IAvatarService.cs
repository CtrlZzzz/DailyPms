namespace ClientServices.Interfaces
{
    public interface IAvatarService
    {
        Task<Stream?> GetImageStreamAsync(string studentId);
        Task<string?> GetImageUri(string studentId);
    }
}
