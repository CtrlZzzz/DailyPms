using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsAPI.Data
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<School>> GetAllSchoolsAsync();

        Task<School> GetSchoolByIdAsync(string id);

        Task<School> GetSchoolByNameAsync(string name);

        Task UpdateSchoolByIdAsync(string id, School updatedSchool);

        Task CreateSchoolAsync(School newSchool);

        Task DeleteSchoolByIdAsync(string id);
    }
}
