using System;
using DailyPmsShared.DTOs;

namespace ClientServices.Interfaces
{
	public interface ISchoolService
	{
		Task<IEnumerable<SchoolSummary>> GetSchoolSummariesAsync();
	}
}

