using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Models;

namespace DailyPmsAPI.Data
{
    public interface IClasseRepository
    {
        Task<IEnumerable<Classe>> GetAllClassesBySchoolAsync(string schoolId);

        Task<Classe> GetClasseByIdAsync(string id);

        Task<Classe> GetClasseByNameAsync(string name, string schoolId);

        Task CreateClasseAsync(Classe newClasse);

        Task UpdateClasseByIdAsync(string id, Classe updatedClasse);

        Task DeleteClasseByIdAsync(string id);
    }
}
