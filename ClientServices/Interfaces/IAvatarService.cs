namespace ClientServices.Interfaces
{
    public interface IAvatarService
    {
        Task<Stream?> GetImageStreamAsync(string studentId);
        Task<string?> GetImageUriAsync(string studentId);
    }
}
