using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsClient.Services
{
    public interface IStudentDataService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();

        Task<Student> GetStudentByIdAsync(string id);

        Task CreateStudentAsync(Student newStudent);

        Task UpdateStudentByIdAsync(string id, Student updatedStudent);

        Task DeleteStudentByIdAsync(string id);
    }
}
