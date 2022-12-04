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

        group.MapPost("/insert", Insert);

        group.MapPut("/update", Update);

        group.MapDelete("/delete", Delete);

        static async Task<IResult> Get(ISitesService sitesService, Guid id)
        {
            return await sitesService.GetPage(id) is PageViewModel page ? Results.Ok(page) : Results.NotFound();
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
    }
}