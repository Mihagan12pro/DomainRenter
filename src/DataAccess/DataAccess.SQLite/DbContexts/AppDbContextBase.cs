using DomainModels.Domains;
using DomainModels.Receipts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SQLite.DbContexts
{
    public abstract class AppDbContextBase : DbContext
    {
        public DbSet<ReceiptModel> Receipts { get; set; }

        public DbSet<DomainModel> Domains { get; set; }

        public DbSet<RentedDomainModel> RentedDomains { get; set; }

        public AppDbContextBase()
        {
            
        }

        public AppDbContextBase(DbContextOptions options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly()
                );
    }
}
