using DailyPmsShared;

namespace ClientServices.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
    }
}
