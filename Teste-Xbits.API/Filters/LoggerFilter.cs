using Microsoft.AspNetCore.Mvc.Filters;
using Teste_Xbits.API.Extensions;
using Teste_Xbits.Domain.Interface;

namespace Teste_Xbits.API.Filters;

public sealed class LoggerFilter(
    INotificationHandler notification,
    ILoggerHandler logger)
    : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.IsMethodGet()) return;

        if (!SuccessFlow(context)) return;

        if (logger.HasLogger())
            logger.SaveInDataBase().Wait();

        base.OnActionExecuted(context);
    }

    private bool SuccessFlow(ActionExecutedContext context) =>
        context.Exception is null &&
        context.ModelState.IsValid &&
        !notification.HasNotification();
}