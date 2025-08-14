using Teste_Xbits.Domain.Handlers.NotificationHandler;

namespace Teste_Xbits.API.Middlewares;

public sealed class CaptureStatusCodeTooManyRequestsMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        await next(httpContext);

        if (httpContext.Response.StatusCode == StatusCodes.Status429TooManyRequests)
            await httpContext.Response.WriteAsJsonAsync(
                new DomainNotification("Error", "Too Many Requests Server"),
                CancellationToken.None);
    }
    
}