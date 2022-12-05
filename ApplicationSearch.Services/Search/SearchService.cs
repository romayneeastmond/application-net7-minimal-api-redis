using ApplicationSearch.Models;
using ApplicationSearch.Services.Cache;
using ApplicationSearch.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ApplicationSearch.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly SitesDbContext _db;
        private ICacheService _cacheService = default!;

        public SearchService(SitesDbContext db)
        {
            _db = db;
        }

        public async Task<List<PageViewModel>> FindByDatabase(SearchQueryViewModel query)
        {
            var results = new List<PageViewModel>();

            if (string.IsNullOrWhiteSpace(query.Query))
            {
                return results;
            }

            var pages = await _db.Pages.Where(x => x.SiteId == query.SiteId).ToListAsync();

            foreach (var page in pages)
            {
                if (!ContainsByCaseAndFuzzy(page.Content, query.Query))
                {
                    continue;
                }

                results.Add(new PageViewModel
                {
                    Id = page.Id,
                    SiteId = page.SiteId,
                    Url = page.Url,
                    Html = page.Html,
                    Content = page.Content,
                    Created = page.Created,
                    Modified = page.Modified
                });
            }

            return results.ToList();
        }

        public async Task<List<ResultViewModel>> FindByCache(SearchQueryViewModel query)
        {
            var results = new List<ResultViewModel>();

            var keys = _cacheService.Scan($"{query.SiteId}*");

            foreach (var key in keys)
            {
                try
                {
                    var cacheResult = await _cacheService.Get(key);

                    if (string.IsNullOrWhiteSpace(cacheResult) || (!string.IsNullOrWhiteSpace(cacheResult) && !ContainsByCaseAndFuzzy(cacheResult, query.Query)))
                    {
                        continue;
                    }

                    var result = JsonConvert.DeserializeObject<ResultViewModel>(cacheResult);

                    if (result != null)
                    {
                        results.Add(result);
                    }
                }
                catch
                {
                    //ignored
                }
            }

            return results.ToList();
        }

        public SearchService SetCacheService(ICacheService cacheService)
        {
            _cacheService = cacheService;

            return this;
        }

        private bool ContainsByCaseAndFuzzy(string content, string query)
        {
            var regex = new Regex($@"{query}(.{{0,2}})?", RegexOptions.IgnoreCase);

            if (!content.Contains(query, StringComparison.OrdinalIgnoreCase) && !regex.IsMatch(content))
            {
                return false;
            }

            return true;
        }
    }
}
