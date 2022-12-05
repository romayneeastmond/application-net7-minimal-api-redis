using ApplicationSearch.Services.Cache;
using ApplicationSearch.Services.Search;
using ApplicationSearch.Services.ViewModels;

public static class Search
{
    public static void AddRoutesSearch(this WebApplication app)
    {
        var group = app.MapGroup("/search").AddEndpointFilter(async (context, next) =>
        {
            return await next(context);
        });

        group.MapGet("/find/in/database", SearchDatabase);

        group.MapGet("/find/in/cache", SearchCache);

        static async Task<IResult> SearchDatabase(ISearchService searchService, [AsParameters] SearchQueryViewModel query)
        {
            return await searchService.FindByDatabase(query) is IEnumerable<PageViewModel> results ? Results.Ok(results) : Results.NotFound();
        };

        static async Task<IResult> SearchCache(ISearchService searchService, ICacheService cacheService, [AsParameters] SearchQueryViewModel query)
        {
            return await searchService.SetCacheService(cacheService).FindByCache(query) is IEnumerable<ResultViewModel> results ? Results.Ok(results) : Results.NotFound();
        };
    }
}
