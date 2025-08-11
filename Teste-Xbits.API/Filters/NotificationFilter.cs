using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Teste_Xbits.API.Extensions;
using Teste_Xbits.Domain.Interface;

namespace Teste_Xbits.API.Filters;

public sealed class NotificationFilter(
    INotificationHandler notificationHandler)
    : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (!context.IsMethodGet() && notificationHandler.HasNotification())
            context.Result = new BadRequestObjectResult(notificationHandler.GetNotifications());

        base.OnActionExecuted(context);
    }
}