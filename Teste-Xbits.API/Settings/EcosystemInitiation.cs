using Teste_Xbits.API.Middlewares;
using Teste_Xbits.API.Settings.Constants;
using Teste_Xbits.Domain.Providers;

namespace Teste_Xbits.API.Settings;

public static class EcosystemInitiation
{
    public static void AddWebApplication(this WebApplication app, IConfiguration configuration)
    {
        var environmentConfiguration = LoadEnvironmentConfiguration(configuration);

        ConfigureSwagger(app, environmentConfiguration);
        ConfigureMiddlewares(app);
        ConfigureEndpoints(app);
    }

    private static EnvironmentConfigurationOptions LoadEnvironmentConfiguration(IConfiguration configuration) =>
        configuration.GetSection(EnvironmentConfigurationOptions.SectionName).Get<EnvironmentConfigurationOptions>()!;

    private static void ConfigureSwagger(WebApplication app, EnvironmentConfigurationOptions config)
    {
        if (!config.Active) return;

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    private static void ConfigureMiddlewares(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseMiddleware<GlobalNotificationHandlingMiddleware>();
        }

        app.UseMiddleware<CaptureStatusCodeTooManyRequestsMiddleware>();
        app.UseHttpsRedirection();
        app.UseCors(CorsName.CorsPolicy);
        app.UseWebSockets();
        app.UseRateLimiter();
        app.UseAuthentication();
        app.UseAuthorization();
    }

    private static void ConfigureEndpoints(WebApplication app)
    {
        app.MapHealthChecks("/health");
        app.MapControllers();
    }
}