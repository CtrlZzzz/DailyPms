using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsClient.Services
{
    public class StudentDataService : IStudentDataService
    {
        public StudentDataService()
        {
        }


        public Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task CreateStudentAsync(Student newStudent)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentByIdAsync(string id, Student updatedStudent)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudentByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
