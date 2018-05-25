using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Staff.Infrastructure.Data;
using Xunit;

namespace TestDbContext
{
    public class SeedTest
    {
        [Fact]
        public void TestEnsureSeedData()
        {
            var envVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{envVariable}.json", optional: true)
                .Build();
            var connStr = config.GetConnectionString("StaffDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<StaffContext>();
            optionsBuilder.UseSqlServer(connStr);

            using (var ctx = new StaffContext(optionsBuilder.Options))
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
                ctx.EnsureSeedData();
            }
        }
    }
}
