using ApplicationSearch.Services.Cache;
using ApplicationSearch.Services.ViewModels;
using Newtonsoft.Json;

public static class Cache
{
    public static void AddRoutesCache(this WebApplication app)
    {
        var group = app.MapGroup("/cache").AddEndpointFilter(async (context, next) =>
        {
            return await next(context);
        });

        group.MapPost("/set", Set);

        group.MapGet("/get", Get);

        group.MapGet("/scan", Scan);

        group.MapDelete("/delete", Delete);

        static async Task<IResult> Set(ICacheService cacheService, string key, ResultViewModel value)
        {
            await cacheService.Set(key, JsonConvert.SerializeObject(value));

            return Results.Ok();
        };

        static async Task<IResult> Get(ICacheService cacheService, string key)
        {
            var value = await cacheService.Get(key);

            try
            {
                return Results.Ok(JsonConvert.DeserializeObject<ResultViewModel>(value));
            }
            catch
            {
                return Results.Ok(value);
            }
        };

        static IResult Scan(ICacheService cacheService, string pattern)
        {
            var results = cacheService.Scan(pattern);

            return Results.Ok(results);
        }

        static async Task<IResult> Delete(ICacheService cacheService, string key)
        {
            await cacheService.Delete(key);

            return Results.Ok();
        };
    }
}