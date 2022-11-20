using Azure;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSearch.Models
{
    public class SitesDbContext : DbContext, ISitesDbContext
    {
        public SitesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Site> Sites { get; set; }

        public DbSet<SiteSource> SiteSources { get; set; }

        public DbSet<Page> Pages { get; set; }
    }
}
