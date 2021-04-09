using System;
using System.Collections.Generic;
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


        public IEnumerable<Classe> GetAllClassesBySchool(string schoolId)
        {
            return dbContext.Classes.Find(Classe => Classe.SchoolID == schoolId).ToList();
        }

        public Classe GetClasseById(string id)
        {
            return dbContext.Classes.Find(classe => classe.ClasseID == id).FirstOrDefault();
        }

        public Classe GetClasseByName(string name)
        {
            return dbContext.Classes.Find(classe => classe.Name == name).FirstOrDefault();
        }

        public void CreateClasse(Classe newClasse)
        {
            if (newClasse == null)
            {
                throw new ArgumentNullException(nameof(newClasse));
            }

            dbContext.Classes.InsertOne(newClasse);
        }

        public void UpdateClasseById(string id, Classe updatedClasse)
        {
            if (updatedClasse == null)
            {
                throw new ArgumentNullException(nameof(updatedClasse));
            }

            dbContext.Classes.ReplaceOne(classe => classe.ClasseID == id, updatedClasse);
        }

        public void DeleteClasseById(string id)
        {
            dbContext.Classes.DeleteOne(classe => classe.ClasseID == id);
        }
    }
}
