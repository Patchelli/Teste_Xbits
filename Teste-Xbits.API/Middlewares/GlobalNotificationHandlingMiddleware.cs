using Teste_Xbits.Domain.Handlers.NotificationHandler;

namespace Teste_Xbits.API.Middlewares;

public sealed class GlobalNotificationHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalNotificationHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(
                new DomainNotification("Sever side error", e.Message),
                CancellationToken.None);
        }
    }
}