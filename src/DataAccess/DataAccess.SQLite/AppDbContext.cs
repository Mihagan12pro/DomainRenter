using DomainModels.Domains;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.SQLite
{
    internal class AppDbContext : DbContext
    {
        public DbSet<RentedDomainModel> Domains { get; set; }

        public DbSet<RentedDomainModel> RentedDomains { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("database.db");

            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly()
                );
    }
}
