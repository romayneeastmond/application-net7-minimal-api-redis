using ApplicationSearch.Models;
using ApplicationSearch.Services.ViewModels;

namespace ApplicationSearch.Services.Sites
{
    public interface ISitesService
    {
        Task<SiteViewModel> Get(Guid id);

        Task<SiteSourceViewModel> GetSiteSource(Guid id);

        Task<PageViewModel> GetPage(Guid id);

        Task<SiteViewModel> Insert(Site site);

        Task<SiteSourceViewModel> InsertSiteSource(SiteSource siteSource);

        Task<PageViewModel> InsertPage(Page page);

        Task Update(Site site);

        Task UpdateSiteSource(SiteSource siteSource);

        Task UpdatePage(Page page);

        Task Delete(Guid id);

        Task DeleteSiteSource(Guid id);

        Task DeletePage(Guid id);
    }
}
