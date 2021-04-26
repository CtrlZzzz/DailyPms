using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;
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


        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await dbContext.Students.Find(student => true).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsBySchoolAsync(string schoolId)
        {
            return await dbContext.Students.Find(student => student.SchoolID == schoolId).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsByClasseAsync(string classeId, string schoolId)
        {
            return await dbContext.Students.Find(student => student.ClasseID == classeId && student.SchoolID == schoolId).ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(string id)
        {
            return await dbContext.Students.Find(student => student.StudentID == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByNameAsync(string name)
        {
            return await dbContext.Students.Find(student => student.LastName == name).ToListAsync();
        }

        public async Task CreateStudentAsync(Student newStudent)
        {
            if (newStudent == null)
            {
                throw new ArgumentNullException(nameof(newStudent));
            }

            await dbContext.Students.InsertOneAsync(newStudent);
        }

        public async Task UpdateStudentByIdAsync(string id, Student updatedStudent)
        {
            if (updatedStudent == null)
            {
                throw new ArgumentNullException(nameof(updatedStudent));
            }

            await dbContext.Students.ReplaceOneAsync(student => student.StudentID == id, updatedStudent);
        }

        public async Task DeleteStudentByIdAsync(string id)
        {
            await dbContext.Students.DeleteOneAsync(student => student.StudentID == id);
        }
    }
}
