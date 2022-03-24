using System;
using System.Threading.Tasks;
using DailyPmsShared;
using MongoDB.Driver;

namespace DailyPmsAPI.Repositories
{
    public class MongoRepository<T> : IGetByNameRepository<T> where T : IEntity
    {
        public MongoRepository(IMongoDatabase db, string collectionName)
        {
            if (db == null) throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException(nameof(collectionName));

            Collection = db.GetCollection<T>(collectionName);
        }

        protected IMongoCollection<T> Collection { get; set; }


        public Task<T> CreateAsync(T newItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(string id, T item)
        {
            throw new NotImplementedException();
        }
    }
}

