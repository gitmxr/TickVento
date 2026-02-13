using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TickVento.Infrastructure.Persistence.Data
{
    public class TickVentoDbContextFactory : IDesignTimeDbContextFactory<TickVentoDbContext>
    {
        public TickVentoDbContext CreateDbContext(string[] args)
        {
            // Build config from API project
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..\\TickVento.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TickVentoDbContext>();
            var connectionString = configuration.GetConnectionString("TickVentoConnectionString");
            builder.UseSqlServer(connectionString);

            return new TickVentoDbContext(builder.Options);
        }
    }
}
