using DomainModels.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.SQLite.Configurations
{
    internal class RentedDomainsConfiguration : IEntityTypeConfiguration<RentedDomainModel>
    {
        public void Configure(EntityTypeBuilder<RentedDomainModel> builder)
        {
            builder
                .HasOne(rd => rd.Domain)
                .WithOne(d => d.RentedDomain)
                .HasForeignKey<RentedDomainModel>(rd => rd.DomainId);
        }
    }
}
