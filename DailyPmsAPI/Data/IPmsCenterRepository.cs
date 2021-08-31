using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsAPI.Data
{
    public interface IPmsCenterRepository
    {
        Task<IEnumerable<PmsCenter>> GetAllCentersAsync();

        Task<PmsCenter> GetCenterByIdAsync(string id);

        Task<PmsCenter> GetCenterByNameAsync(string name);

        Task UpdateCenterByIdAsync(string id, PmsCenter updatedCenter);

        Task CreateCenterAsync(PmsCenter newCenter);

        Task DeleteCenterByIdAsync(string id);
    }
}
