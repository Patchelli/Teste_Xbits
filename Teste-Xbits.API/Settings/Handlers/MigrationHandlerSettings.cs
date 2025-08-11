using Microsoft.EntityFrameworkCore;
using Teste_Xbits.Infra.ORM.Context;

namespace Teste_Xbits.API.Settings.Handlers;

public static class MigrationHandlerSettings
{
    public static async Task MigrateDatabaseAsync(this WebApplication webApp, IConfiguration configuration)
    {
        using var scope = webApp.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        await appContext.Database.MigrateAsync();

        var seedHandler = new DbInitializer(appContext);

        await seedHandler.Seeding();
    }
}