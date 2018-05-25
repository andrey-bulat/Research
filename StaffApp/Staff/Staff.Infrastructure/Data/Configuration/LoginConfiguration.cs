using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Staff.Model.Entities;

namespace Staff.Infrastructure.Data.Configuration
{
    internal class LoginConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(o => o.LoginId);

            builder.Property(o => o.LoginId).HasColumnName("Id").IsRequired();
            builder.Property(o => o.Login).HasColumnName("Login").HasColumnType("varchar(50)").IsRequired();
            builder.Property(o=>o.Password).HasColumnName("Password").HasColumnType("varchar(20)").IsRequired();

            builder.HasOne(u=>u.Employee).WithOne(e=>e.User).HasForeignKey<AppUser>(u=>u.LoginId);
        }
    }
}
