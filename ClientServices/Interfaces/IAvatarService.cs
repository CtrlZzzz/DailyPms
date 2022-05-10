namespace ClientServices.Interfaces
{
    public interface IAvatarService
    {
        Task<Stream?> GetImageStreamAsync(string studentId);
        Task<Uri?> GetImageUri(string studentId);
    }
}
