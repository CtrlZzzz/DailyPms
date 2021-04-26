using System.Threading.Tasks;
using DailyPmsShared;

namespace DailyPmsAPI.Data
{
    public interface IPmsFileRepository
    {
        Task<PmsFile> GetPmsFileByIdAsync(string id);

        Task<PmsFile> GetPmsFileByStudentIdAsync(string studentId);

        Task CreatePmsFileAsync(PmsFile newPmsFile);

        Task UpDatePmsFileAsync(string id, PmsFile updatedPmsFile);

        Task DeletePmsFileAsync(string id);
    }
}
