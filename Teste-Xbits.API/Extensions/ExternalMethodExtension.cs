using Microsoft.AspNetCore.Mvc.Filters;

namespace Teste_Xbits.API.Extensions;

public static class ExternalMethodExtension
{
    private const string MethodGet = "GET";
    
    public static bool IsMethodGet(this FilterContext context) => context.HttpContext.Request.Method == MethodGet;
}