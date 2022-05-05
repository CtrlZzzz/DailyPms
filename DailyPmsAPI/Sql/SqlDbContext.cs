using DailyPmsShared;
using Microsoft.EntityFrameworkCore;
using DailyPmsApi.Model;

namespace DailyPmsAPI.Sql
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public DbSet<StudentPicture> StudentPictures { get; set; }

        public DbSet<DailyPmsApi.Model.StudentPictureTest> StudentPictureTest { get; set; }
    }
}

