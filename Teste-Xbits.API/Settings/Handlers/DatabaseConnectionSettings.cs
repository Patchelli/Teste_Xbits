using Microsoft.EntityFrameworkCore;
using Teste_Xbits.Domain.Providers;
using Teste_Xbits.Infra.ORM.Context;

namespace Teste_Xbits.API.Settings.Handlers;

public static class DatabaseConnectionSettings
{
    public static void AddDatabaseConnectionSettings(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>((serviceProv, options) =>
            options.UseSqlServer(
                serviceProv.GetRequiredService<ConnectionStringOptions>().DefaultConnection,
                sql => sql.CommandTimeout(180)));
    }
}