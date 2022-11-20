using ApplicationSearch.Models;
using Microsoft.EntityFrameworkCore;

public static class SitesDbContextEnableMigrations
{
    public static void EnableMigrations(this WebApplication app)
    {
        var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<SitesDbContext>();

        db.Database.Migrate();
    }
}
