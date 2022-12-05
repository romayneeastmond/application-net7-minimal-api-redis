using ApplicationSearch.Services.Cache;
using ApplicationSearch.Services.ViewModels;

namespace ApplicationSearch.Services.Search
{
    public interface ISearchService
    {
        Task<List<ResultViewModel>> FindByCache(SearchQueryViewModel query);

        Task<List<PageViewModel>> FindByDatabase(SearchQueryViewModel query);

        SearchService SetCacheService(ICacheService cacheService);
    }
}
