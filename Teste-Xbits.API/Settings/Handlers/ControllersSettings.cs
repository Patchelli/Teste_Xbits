using System.Text.Json.Serialization;

namespace Teste_Xbits.API.Settings.Handlers;

public static class ControllersSettings
{
    public static void AddControllersSettings(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    }
}