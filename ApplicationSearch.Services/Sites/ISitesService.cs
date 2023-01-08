using ApplicationSearch.Models;
using ApplicationSearch.Services.ViewModels;

namespace ApplicationSearch.Services.Sites
{
    public interface ISitesService
    {
        Task<SiteViewModel> Get(Guid id);

        Task<SiteSourceViewModel> GetSiteSource(Guid id);

        Task<PageViewModel> GetPage(Guid id);

        Task<int> GetPagesCount(Guid siteId);

        Task<List<string>> GetPageUrls(Guid siteId);

        Task<List<string>> GetPageUrls(Guid siteId, List<string> urls);

        Task<SiteViewModel> Insert(Site site);

        Task<SiteSourceViewModel> InsertSiteSource(SiteSource siteSource);

        Task<PageViewModel> InsertPage(Page page);

        Task Update(Site site);

        Task UpdateSiteSource(SiteSource siteSource);

        Task UpdatePage(Page page);

        Task RebuildPages(Guid siteId, string endPointUrl);

        Task RebuildPages(Guid siteId, string endPointUrl, int count);

        Task Delete(Guid id);

        Task DeleteSiteSource(Guid id);

        Task DeletePage(Guid id);
    }
}
