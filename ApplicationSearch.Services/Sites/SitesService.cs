using ApplicationSearch.Models;
using ApplicationSearch.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSearch.Services.Sites
{
    public class SitesService : ISitesService
    {
        private readonly SitesDbContext _db;

        public SitesService(SitesDbContext db)
        {
            _db = db;
        }

        public async Task<SiteViewModel> Get(Guid id)
        {
            var site = await _db.Sites.FindAsync(id);

            if (site == null)
            {
                return new SiteViewModel();
            }

            var siteSources = await _db.SiteSources.Where(x => x.SiteId == site.Id).ToListAsync();

            var siteViewModel = new SiteViewModel
            {
                Id = site.Id,
                Name = site.Name,
                Created = site.Created,
                Modified = site.Modified,
                Sources = siteSources.Select(x => new SiteSourceViewModel
                {
                    Id = x.Id,
                    SiteId = x.SiteId,
                    Url = x.Url,
                    Created = x.Created,
                }).ToList()
            };

            return siteViewModel;
        }

        public async Task<SiteSourceViewModel> GetSiteSource(Guid id)
        {
            var siteSource = await _db.SiteSources.FindAsync(id);

            if (siteSource == null)
            {
                return new SiteSourceViewModel();
            }

            return new SiteSourceViewModel
            {
                Id = siteSource.Id,
                SiteId = siteSource.SiteId,
                Url = siteSource.Url,
                Created = siteSource.Created
            };
        }

        public async Task<PageViewModel> GetPage(Guid id)
        {
            var page = await _db.Pages.FindAsync(id);

            if (page == null)
            {
                return new PageViewModel();
            }

            return new PageViewModel
            {
                Id = page.Id,
                SiteId = page.SiteId,
                Url = page.Url,
                Html = page.Html,
                Title = page.Title,
                Content = page.Content,
                Created = page.Created,
                Modified = page.Modified
            };
        }

        public async Task<List<string>> GetPageUrls(Guid siteId)
        {
            var pages = await _db.Pages.Where(x => x.SiteId == siteId).ToListAsync();

            if (pages.Any())
            {
                return pages.Select(x => x.Url).Distinct().ToList();
            }

            return new List<string>();
        }

        public async Task<List<string>> GetPageUrls(Guid siteId, List<string> urls)
        {
            var pages = await _db.Pages.Where(x => x.SiteId == siteId).ToListAsync();

            if (!pages.Any())
            {
                return urls;
            }

            var pagesUrls = pages.Select(x => x.Url).Distinct().ToList();

            return urls.Where(x => !pagesUrls.Contains(x, StringComparer.OrdinalIgnoreCase)).ToList();
        }

        public async Task<SiteViewModel> Insert(Site site)
        {
            _db.Sites.Add(site);

            await _db.SaveChangesAsync();

            return await Get(site.Id);
        }

        public async Task<SiteSourceViewModel> InsertSiteSource(SiteSource siteSource)
        {
            _db.SiteSources.Add(siteSource);

            await _db.SaveChangesAsync();

            return await GetSiteSource(siteSource.Id);
        }

        public async Task<PageViewModel> InsertPage(Page page)
        {
            if (page.Id != Guid.Empty)
            {
                var temporaryPage = await _db.Pages.Where(x => x.Id == page.Id).FirstOrDefaultAsync();

                if (temporaryPage != null)
                {
                    await UpdatePage(page);

                    return await GetPage(page.Id);
                }
            }

            if (page.SiteId != Guid.Empty)
            {
                var temporaryPage = await _db.Pages.Where(x => x.SiteId == page.SiteId && x.Url.ToLower().Trim() == page.Url.ToLower().Trim()).FirstOrDefaultAsync();

                if (temporaryPage != null)
                {
                    page.Id = temporaryPage.Id;

                    await UpdatePage(page);

                    return await GetPage(page.Id);
                }
            }

            _db.Pages.Add(page);

            await _db.SaveChangesAsync();

            return await GetPage(page.Id);
        }

        public async Task Update(Site site)
        {
            var siteItem = await _db.Sites.FindAsync(site.Id);

            if (siteItem == null)
            {
                throw new KeyNotFoundException(site.Id.ToString());
            }

            siteItem.Name = site.Name;
            siteItem.Modified = DateTime.Now;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateSiteSource(SiteSource siteSource)
        {
            var siteSourceItem = await _db.SiteSources.FindAsync(siteSource.Id);

            if (siteSourceItem == null)
            {
                throw new KeyNotFoundException(siteSource.Id.ToString());
            }

            siteSourceItem.SiteId = siteSource.SiteId;
            siteSourceItem.Url = siteSource.Url;

            await _db.SaveChangesAsync();
        }

        public async Task UpdatePage(Page page)
        {
            var pageItem = await _db.Pages.FindAsync(page.Id);

            if (pageItem == null)
            {
                throw new KeyNotFoundException(page.Id.ToString());
            }

            pageItem.SiteId = page.SiteId;
            pageItem.Url = page.Url;
            pageItem.Html = page.Html;
            pageItem.Title = page.Title;
            pageItem.Content = page.Content;
            pageItem.Modified = DateTime.Now;

            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var site = await _db.Sites.FindAsync(id);

            if (site == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            var siteSources = await _db.SiteSources.Where(x => x.SiteId == site.Id).ToListAsync();
            var pages = await _db.Pages.Where(x => x.SiteId == site.Id).ToListAsync();

            _db.SiteSources.RemoveRange(siteSources);
            _db.Pages.RemoveRange(pages);
            _db.Sites.Remove(site);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteSiteSource(Guid id)
        {
            var siteSource = await _db.SiteSources.FindAsync(id);

            if (siteSource == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            _db.SiteSources.Remove(siteSource);

            await _db.SaveChangesAsync();
        }

        public async Task DeletePage(Guid id)
        {
            var page = await _db.Pages.FindAsync(id);

            if (page == null)
            {
                throw new KeyNotFoundException(id.ToString());
            }

            _db.Pages.Remove(page);

            await _db.SaveChangesAsync();
        }
    }
}
