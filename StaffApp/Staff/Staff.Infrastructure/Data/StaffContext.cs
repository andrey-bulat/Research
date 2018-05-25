using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Staff.Infrastructure.Data.Configuration;
using Staff.Model.Entities;

namespace Staff.Infrastructure.Data
{

    public class StaffContext : DbContext
    {
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<AppUser> Users => Set<AppUser>();

        public StaffContext(DbContextOptions<StaffContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new LoginConfiguration());
        }
    }
}
