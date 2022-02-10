using DailyPmsShared;

namespace ClientServices.Interfaces
{
    public interface ISchoolService
	{
		Task<IEnumerable<School>> GetSchoolSummariesAsync();
	}
}

