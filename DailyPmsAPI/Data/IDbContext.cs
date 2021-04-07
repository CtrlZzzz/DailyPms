using System;
using MongoDB.Driver;
using DailyPmsAPI.Models;

namespace DailyPmsAPI.Data
{
    public interface IDbContext
    {
        IMongoCollection<School> Schools { get; }
        IMongoCollection<Classe> Classes { get; }
        IMongoCollection<Student> Students { get; }
        IMongoCollection<PmsFile> PmsFiles { get; }
    }
}
