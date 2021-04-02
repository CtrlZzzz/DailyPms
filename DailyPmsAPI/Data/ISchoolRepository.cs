using System.Collections.Generic;
using DailyPmsAPI.Models;

namespace DailyPmsAPI.Data
{
    public interface ISchoolRepository
    {
        IEnumerable<School> GetAllSchools();

        School GetSchoolById(string id);

        //void UpdateSchool(School schoolToUpdate, School updatedSchool);
        void UpdateSchoolById(string id, School updatedSchool);
    }
}
