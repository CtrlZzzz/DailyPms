using DailyPmsShared;
using Microsoft.EntityFrameworkCore;

namespace DailyPmsAPI.Sql
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public DbSet<StudentPicture> StudentPictures { get; set; }
    }
}

