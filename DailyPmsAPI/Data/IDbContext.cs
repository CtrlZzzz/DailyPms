using System;
using MongoDB.Driver;
using DailyPmsAPI.Models;

namespace DailyPmsAPI.Data
{
    public interface IDbContext
    {
        IMongoCollection<School> Schools { get; }
    }
}
