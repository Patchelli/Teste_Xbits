using Teste_Xbits.API.Settings.Constants;

namespace Teste_Xbits.API.Settings.Handlers;

public static class CorsSettings
{
    public static void AddCorsSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsName.CorsPolicy, builder =>
            {
                builder.WithMethods()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials();
            });
        });
    }
}