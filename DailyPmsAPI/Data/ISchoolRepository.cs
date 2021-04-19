using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Models;

namespace DailyPmsAPI.Data
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<School>> GetAllSchoolsAsync();

        Task<School> GetSchoolByIdAsync(string id);

        Task<School> GetSchoolByNameAsync(string name);

        Task UpdateSchoolByIdAsync(string id, School updatedSchool);
    }
}
