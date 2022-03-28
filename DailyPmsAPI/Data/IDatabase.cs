using MongoDB.Driver;

namespace DailyPmsAPI.Data
{
    public interface IDatabase
    {
        IMongoDatabase PmsDb { get; }   
    }
}
