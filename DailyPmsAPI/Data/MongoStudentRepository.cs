using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Models;
using MongoDB.Driver;

namespace DailyPmsAPI.Data
{
    public class MongoStudentRepository : IStudentRepository
    {
        readonly IDbContext dbContext;

        public MongoStudentRepository(IDbContext databaseContext)
        {
            dbContext = databaseContext;
        }


        public Task CreateStudentAsync(Student newStudent)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudentByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAllStudentsBySchoolAsync(string schoolId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetStudentByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentByIdAsync(string id, Student updatedStudent)
        {
            throw new NotImplementedException();
        }
    }
}
