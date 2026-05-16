using DomainModels.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.SQLite.Configurations
{
    internal class DomainsConfiguration : IEntityTypeConfiguration<DomainModel>
    {
        public void Configure(EntityTypeBuilder<DomainModel> builder)
        {
            builder.HasIndex(d => d.Name)
                .IsUnique();

            builder.Property(d => d.Name)
                .UseCollation("NOCASE");
        }
    }
}
