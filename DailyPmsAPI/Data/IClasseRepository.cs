using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsAPI.Data
{
    public interface IClasseRepository
    {
        Task<IEnumerable<Class>> GetAllClassesBySchoolAsync(string schoolId);

        Task<Class> GetClasseByIdAsync(string id);

        Task<Class> GetClasseByNameAsync(string name, string schoolId);

        Task CreateClasseAsync(Class newClasse);

        Task UpdateClasseByIdAsync(string id, Class updatedClasse);

        Task DeleteClasseByIdAsync(string id);
    }
}
