using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsAPI.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T newItem);
        Task<T> UpdateAsync(string id, T item);
        Task<bool> DeleteAsync(string id);
    }

    public interface IGetByNameRepository<T> : IRepository<T> where T : IEntity
    {
        Task<T> GetByNameAsync(string name);
    }

    public interface IGetAllRepository<T> : IGetByNameRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
    }

    public interface IGetAllByIdRepository<T> : IGetByNameRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllByIdAsync(string id);
    }
}

