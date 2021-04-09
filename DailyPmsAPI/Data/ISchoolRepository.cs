using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsAPI.Models;

namespace DailyPmsAPI.Data
{
    public interface ISchoolRepository
    {
        IEnumerable<School> GetAllSchools();

        School GetSchoolById(string id);

        void UpdateSchoolById(string id, School updatedSchool);
    }
}
