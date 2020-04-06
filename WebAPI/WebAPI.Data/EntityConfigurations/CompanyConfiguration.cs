using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Data.EntityConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder
                .HasKey(company => company.Id);

            builder
                .Property(company => company.Name)
                .HasMaxLength(100);
        }
    }
}