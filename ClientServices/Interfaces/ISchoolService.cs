using DailyPmsShared;

namespace ClientServices.Interfaces
{
    public interface ISchoolService
    {
        Task<IEnumerable<School>> GetSchoolSummariesAsync();

        Task<School> GetSchoolByIdAsync(string id);

        Task EditSchoolByIdAsync(string id, School editedSchool);
    }
}

