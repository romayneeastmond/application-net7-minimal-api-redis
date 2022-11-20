using Microsoft.EntityFrameworkCore;

namespace ApplicationSearch.Models
{
    public interface ISitesDbContext
    {
        DbSet<Site> Sites { get; set; }

        DbSet<SiteSource> SiteSources { get; set; }

        DbSet<Page> Pages { get; set; }
    }
}
