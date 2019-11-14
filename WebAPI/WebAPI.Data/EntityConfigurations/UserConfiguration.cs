using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(user => user.Id);

            builder
                .Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(user => user.Surname)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .HasOne(user => user.Company)
                .WithMany(company => company.Users)
                .HasForeignKey(x => x.CompanyId);
        }
    }
}
