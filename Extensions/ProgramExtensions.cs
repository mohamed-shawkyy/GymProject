using Gym.DAL;
using Gym.DAL.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace Gym.Extensions
{
    public static class ProgramExtensions
    {
        public static async Task IntializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("Migrations Added to Database");
            }
            //D:\.net Tasks\mvc\Gym\wwwroot\Files\plans.json
            var folderPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "files");
            await DataSeeder.SeedAsync(dbContext, logger, folderPath);
        }
    }
}
