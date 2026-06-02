using Microsoft.EntityFrameworkCore;

namespace DataAccess.SQLite.DbContexts
{
    public class AppDbContext : AppDbContextBase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
            SQLitePCL.Batteries.Init();
        }

        public AppDbContext()
        {
            Database.Migrate();
        }
    }
}
