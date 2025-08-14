using Microsoft.AspNetCore.Mvc.Filters;
using Teste_Xbits.API.Extensions;
using Teste_Xbits.Domain.Interface;

namespace Teste_Xbits.API.Filters;

public sealed class UnitOfWorkFilter(
    IUnitOfWork unitOfWork,
    INotificationHandler notificationHandler)
    : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.IsMethodGet()) return;

        var routeName = context.GetEndPointName();

        if (IsAuthenticatorRoute(routeName))
            AuthenticationMethod(context);
        else
            OthersMethods(context);

        base.OnActionExecuted(context);
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.IsMethodGet()) return;

        unitOfWork.BeginTransaction();

        base.OnActionExecuting(context);
    }


    private static bool IsAuthenticatorRoute(string? routeName)
    {
        const string transfer = "GenerateAccessToken";

        return routeName is not null && routeName.Equals(transfer, StringComparison.InvariantCulture);
    }

    private void AuthenticationMethod(ActionExecutedContext context)
    {
        if (context.Exception is null && context.ModelState.IsValid)
            unitOfWork.CommitTransaction();
        else
            unitOfWork.RollbackTransaction();
    }

    private void OthersMethods(ActionExecutedContext context)
    {
        if (SuccessFlow(context))
            unitOfWork.CommitTransaction();
        else
            unitOfWork.RollbackTransaction();
    }

    private bool SuccessFlow(ActionExecutedContext context) =>
        context.Exception is null &&
        context.ModelState.IsValid &&
        !notificationHandler.HasNotification();
}