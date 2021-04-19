using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Models;
using MongoDB.Driver;

namespace DailyPmsAPI.Data
{
    public class MongoClasseRepository : IClasseRepository
    {
        readonly IDbContext dbContext;

        public MongoClasseRepository(IDbContext databaseContext)
        {
            dbContext = databaseContext;
        }


        public async Task<IEnumerable<Classe>> GetAllClassesBySchoolAsync(string schoolId)
        {
            return await dbContext.Classes.Find(Classe => Classe.SchoolID == schoolId).ToListAsync();
        }

        public async Task<Classe> GetClasseByIdAsync(string id)
        {
            return await dbContext.Classes.Find(classe => classe.ClasseID == id).FirstOrDefaultAsync();
        }

        public async Task<Classe> GetClasseByNameAsync(string name, string schoolId)
        {
            return await dbContext.Classes.Find(classe => classe.Name == name && classe.SchoolID == schoolId).FirstOrDefaultAsync();
        }

        public async Task CreateClasseAsync(Classe newClasse)
        {
            if (newClasse == null)
            {
                throw new ArgumentNullException(nameof(newClasse));
            }

            await dbContext.Classes.InsertOneAsync(newClasse);
        }

        public async Task UpdateClasseByIdAsync(string id, Classe updatedClasse)
        {
            if (updatedClasse == null)
            {
                throw new ArgumentNullException(nameof(updatedClasse));
            }

            await dbContext.Classes.ReplaceOneAsync(classe => classe.ClasseID == id, updatedClasse);
        }

        public async Task DeleteClasseByIdAsync(string id)
        {
            await dbContext.Classes.DeleteOneAsync(classe => classe.ClasseID == id);
        }
    }
}
