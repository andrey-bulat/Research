using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Staff.Model.Entities;

namespace Staff.Infrastructure.Data.Configuration
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees", "app");
            builder.HasKey(o => o.EmployeeId);

            builder.Property(o => o.EmployeeId).HasColumnName("EmployeeId").ValueGeneratedOnAdd();
            builder.Property(o => o.FirstName).HasColumnName("FirstName").HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(o => o.SecondName).HasColumnName("SecondName").HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(o => o.LastName).HasColumnName("LastName").HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(o => o.Birthday).HasColumnName("Birthday").HasColumnType("date").IsRequired();
            builder.Property(o => o.MobilePhone).HasColumnName("MobilePhone").HasColumnType("varchar(30)");

            builder.Property(o => o.Department).HasColumnName("Department").HasColumnType("nvarchar(50)");
            builder.Property(o => o.Position).HasColumnName("Position").HasColumnType("nvarchar(50)");
            builder.Property(o => o.WorkPhone).HasColumnName("WorkPhone").HasColumnType("varchar(30)");

        }
    }
}