using ApplicationSearch.Models;
using ApplicationSearch.Services.Sites;
using ApplicationSearch.Services.ViewModels;

public static class Sites
{
    public static void AddRoutesSites(this WebApplication app)
    {
        var group = app.MapGroup("/sites").AddEndpointFilter(async (context, next) =>
        {
            return await next(context);
        });

        group.MapGet("/get", Get);

        group.MapGet("/get/source", GetSource);

        group.MapPost("/insert", Insert);

        group.MapPost("/insert/source", InsertSource);

        group.MapPut("/update", Update);

        group.MapPut("/update/source", UpdateSource);

        group.MapDelete("/delete", Delete);

        group.MapDelete("/delete/source", DeleteSource);

        static async Task<IResult> Get(ISitesService sitesService, Guid id)
        {
            return await sitesService.Get(id) is SiteViewModel site ? Results.Ok(site) : Results.NotFound();
        };

        static async Task<IResult> GetSource(ISitesService sitesService, Guid id)
        {
            return await sitesService.GetSiteSource(id) is SiteSourceViewModel siteSource ? Results.Ok(siteSource) : Results.NotFound();
        };

        static async Task<IResult> Insert(ISitesService sitesService, [AsParameters] Site site)
        {
            await sitesService.Insert(site);

            return Results.Created($"/sites/{site.Id}", site);
        };

        static async Task<IResult> InsertSource(ISitesService sitesService, [AsParameters] SiteSource siteSource)
        {
            await sitesService.InsertSiteSource(siteSource);

            return Results.Created($"/sites/get/source{siteSource.Id}", siteSource);
        };

        static async Task<IResult> Update(ISitesService sitesService, [AsParameters] Site site)
        {
            await sitesService.Update(site);

            return Results.StatusCode(204);
        }

        static async Task<IResult> UpdateSource(ISitesService sitesService, [AsParameters] SiteSource siteSource)
        {
            await sitesService.UpdateSiteSource(siteSource);

            return Results.StatusCode(204);
        }

        static async Task<IResult> Delete(ISitesService sitesService, Guid id)
        {
            await sitesService.Delete(id);

            return Results.StatusCode(204);
        };

        static async Task<IResult> DeleteSource(ISitesService sitesService, Guid id)
        {
            await sitesService.DeleteSiteSource(id);

            return Results.StatusCode(204);
        };
    }
}