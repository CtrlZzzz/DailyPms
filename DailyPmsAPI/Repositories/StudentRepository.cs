using DailyPmsAPI.Data;
using DailyPmsShared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyPmsAPI.Repositories
{
    public class StudentRepository : MongoRepository<Student>, IGetByName<Student>
    {
        public StudentRepository(IDatabase db, string collectionName) 
            : base(db, "Students") {}

        public async Task<IEnumerable<Student>> GetByNameAsync(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            
            var result = await Collection.FindAsync(s => s.LastName.ToLower().Contains(name.ToLower()));
            return await result.ToListAsync();
        }
    }
}
