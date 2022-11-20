using ApplicationSearch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApplicationSearch.Api
{
    public class SitesDbContextFactory : IDesignTimeDbContextFactory<SitesDbContext>
    {
        public SitesDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

            var connectionString = configuration.GetConnectionString("SitesDbString");

            var builder = new DbContextOptionsBuilder<SitesDbContext>();

            builder.UseSqlServer(connectionString);

            return new SitesDbContext(builder.Options);            
        }
    }
}
