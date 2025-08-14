using Teste_Xbits.API.Extensions;
using Teste_Xbits.Domain.Providers;

namespace Teste_Xbits.API.Settings.Handlers;

public static class ProviderSettings
{
    public static void AddProviderSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.SetConfigureOptions<ConnectionStringOptions>(configuration, ConnectionStringOptions.SectionName);
        services.SetConfigureOptions<JwtOptions>(configuration, JwtOptions.SectionName);
    }
}