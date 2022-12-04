using Microsoft.EntityFrameworkCore;
using ApplicationSearch.Models;
using ApplicationSearch.Services.Cache;
using ApplicationSearch.Services.Sites;

var builder = WebApplication.CreateBuilder(args);

var cacheConnectionString = BuilderHelper.GetConfigurationString("CacheConnection", builder);

builder.Services.AddDbContext<SitesDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("SitesDbString"),
        migrations => migrations.MigrationsAssembly("ApplicationSearch.Models")
    )
);

builder.Services.AddScoped<ISitesDbContext>(provider => provider.GetService<SitesDbContext>()!);
builder.Services.AddTransient<ICacheService>(x => new CacheService(cacheConnectionString));
builder.Services.AddTransient<ISitesService, SitesService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Search Results on .NET 7 Framework", Version = "v1" });
});

var app = builder.Build();

app.EnableMigrations();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search Results on .NET 7 Framework"));

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "Search Results on .NET 7 Framework";
});

app.AddRoutesSites();
app.AddRoutesPages();
app.AddRoutesCache();

app.Run();
