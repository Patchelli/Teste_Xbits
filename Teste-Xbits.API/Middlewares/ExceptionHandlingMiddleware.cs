using Teste_Xbits.Domain.Handlers.NotificationHandler;

namespace Teste_Xbits.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        var origin = exception.Data["origin"]?.ToString();

        await context.Response.WriteAsJsonAsync(
            new DomainNotification(origin ?? "Error", exception.Message),
            CancellationToken.None);
    }
}
