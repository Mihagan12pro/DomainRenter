using DomainModels.Domains;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.SQLite
{
    public class AppDbContext : DbContext
    {


        public DbSet<DomainModel> Domains { get; set; }

        public DbSet<RentedDomainModel> RentedDomains { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
            SQLitePCL.Batteries.Init();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly()
                );

        public AppDbContext()
        {
            Database.Migrate();
        }
    }
}
