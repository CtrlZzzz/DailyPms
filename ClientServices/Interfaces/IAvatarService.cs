namespace ClientServices.Interfaces
{
    public interface IAvatarService
    {
        Task<Stream> GetStudentImageAsync(string studentId);
    }
}
