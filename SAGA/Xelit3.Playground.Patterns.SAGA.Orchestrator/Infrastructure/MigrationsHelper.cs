using Microsoft.EntityFrameworkCore;

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure;

public static class MigrationsHelper
{
    public static async Task AutomateDbMigrations(this WebApplication webApplication)
    {
        using (var scope = webApplication.Services.CreateAsyncScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<BillingDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}