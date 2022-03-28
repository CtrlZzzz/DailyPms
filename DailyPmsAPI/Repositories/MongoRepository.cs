using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Data;
using DailyPmsShared;
using MongoDB.Driver;

namespace DailyPmsAPI.Repositories
{
    public abstract class MongoRepository<T> : IRepository<T> where T : class, IEntity
    {
        public MongoRepository(IDatabase db, string collectionName)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException(nameof(collectionName));

            Collection = db.PmsDb.GetCollection<T>(collectionName);
        }

        protected IMongoCollection<T> Collection { get; set; }


        public virtual async Task<T> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var result = await Collection.FindAsync(i => i._id == id);
            return await result.SingleOrDefaultAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await Collection.FindAsync(i => true);
            return await result.ToListAsync();
        }

        public virtual async Task<T> CreateAsync(T newItem)
        {
            if (newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem));
            }

            await Collection.InsertOneAsync(newItem);
            return newItem;
        }

        public virtual async Task<T> UpdateAsync(string id, T item)
        {
            if (string.IsNullOrEmpty(id)) 
                throw new ArgumentNullException(nameof(id));
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var result = await Collection.ReplaceOneAsync(i => i._id == id, item);
            return (result.IsAcknowledged && result.ModifiedCount > 0) ? item : null;
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            if(string.IsNullOrEmpty(id)) 
                throw new ArgumentNullException(nameof(id));
            
            var result = await Collection.DeleteOneAsync(i => i._id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

    }
}

