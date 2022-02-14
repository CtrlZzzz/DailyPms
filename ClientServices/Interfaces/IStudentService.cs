using DailyPmsShared;

namespace ClientServices.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentsAsync();

        Task<IEnumerable<Student>> GetStudentsBySchoolAsync(string schoolId);
    }
}
