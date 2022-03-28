using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsAPI.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T newItem);
        Task<T> UpdateAsync(string id, T item);
        Task<bool> DeleteAsync(string id);
    }

    public interface IGetByName<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetByNameAsync(string name);
    }

    public interface IGetAllFromId<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllFromIdAsync(string id);
    }
}

