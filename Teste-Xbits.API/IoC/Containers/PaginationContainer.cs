using Teste_Xbits.Infra.Interfaces.ServiceContracts;
using Teste_Xbits.Infra.Services;

namespace Teste_Xbits.API.IoC.Containers;

public static class PaginationContainer
{
    public static IServiceCollection AddPaginationContainer(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPaginationQueryService<>), typeof(PaginationQueryService<>));
        return services;
    }
}