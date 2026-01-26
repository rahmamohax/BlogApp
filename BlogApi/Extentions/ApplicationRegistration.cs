using Blog.Domain.Contracts;
using Blog.Persistence.Data.DataSeed;
using Blog.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Web.Extentions
{
    public static class ApplicationRegistration
    {
        public static async Task<WebApplication> MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
            var pending = await db.Database.GetPendingMigrationsAsync();
            if(pending != null) 
                await db.Database.MigrateAsync();
            return app;
        }

        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            await seeder.InitializeAsync();
            return app;
        }

        public static async Task<WebApplication> SeedIdentityDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IIdentityDataSeeder>();
            await seeder.InitializerAsync();
            return app;
        }

    }
}
