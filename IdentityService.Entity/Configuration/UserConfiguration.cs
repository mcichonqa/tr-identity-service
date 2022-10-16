using IdentityService.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Entity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users")
                .HasKey("Id");

            builder
                .Property(e => e.Id)
                .UseIdentityColumn()
                .IsRequired();

            builder
                .Property(x => x.Username)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Password)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.GivenName)
                .HasColumnType("nvarchar")
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(x => x.Surname)
                .HasColumnType("nvarchar")
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(x => x.Gender)
                .HasColumnType("nvarchar")
                .HasMaxLength(6)
                .IsRequired();

            builder
                .Property(x => x.BirthDate)
                .HasColumnType("datetime2")
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(x => x.Role)
                .HasColumnType("nvarchar")
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}