using DataAccess.SQLite.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UnitTests
{
    internal class InMemeoryAppDbContext : AppDbContextBase
    {
        public InMemeoryAppDbContext(DbContextOptions<InMemeoryAppDbContext> options)
            : base(options)
        {
           
        }
    }
}
