using ApplicationSearch.Models;
using ApplicationSearch.Services.Sites;
using ApplicationSearch.Services.ViewModels;

public static class Pages
{
    public static void AddRoutesPages(this WebApplication app)
    {
        var group = app.MapGroup("/pages").AddEndpointFilter(async (context, next) =>
        {
            return await next(context);
        });

        group.MapGet("/get", Get);

        group.MapGet("/get/count", GetCount);

        group.MapPost("/insert", Insert);

        group.MapPut("/update", Update);

        group.MapDelete("/delete", Delete);

        group.MapGet("/urls", GetUrls);

        group.MapPost("/urls/complement", GetUrlsByComplement);

        group.MapPost("/rebuild/cache", RebuildPages);

        static async Task<IResult> Get(ISitesService sitesService, Guid id)
        {
            return await sitesService.GetPage(id) is PageViewModel page ? Results.Ok(page) : Results.NotFound();
        };

        static async Task<IResult> GetCount(ISitesService sitesService, Guid id)
        {
            return await sitesService.GetPagesCount(id) is int count ? Results.Ok(count) : Results.Ok(0);
        };

        static async Task<IResult> GetUrls(ISitesService sitesService, Guid siteId)
        {
            return await sitesService.GetPageUrls(siteId) is List<string> urls ? Results.Ok(urls) : Results.NotFound();
        };

        static async Task<IResult> GetUrlsByComplement(ISitesService sitesService, Guid siteId, List<string> urls)
        {
            return await sitesService.GetPageUrls(siteId, urls) is List<string> complementUrls ? Results.Ok(complementUrls) : Results.Ok(urls);
        };

        static async Task<IResult> Insert(ISitesService sitesService, Page page)
        {
            await sitesService.InsertPage(page);

            return Results.Created($"/pages/{page.Id}", page);
        };

        static async Task<IResult> Update(ISitesService sitesService, Page page)
        {
            await sitesService.UpdatePage(page);

            return Results.StatusCode(204);
        }

        static async Task<IResult> Delete(ISitesService sitesService, Guid id)
        {
            await sitesService.DeletePage(id);

            return Results.StatusCode(204);
        };

        static async Task<IResult> RebuildPages(ISitesService sitesService, Guid siteId, string endPointUrl)
        {
            await sitesService.RebuildPages(siteId, endPointUrl);

            return Results.StatusCode(204);
        }
    }
}