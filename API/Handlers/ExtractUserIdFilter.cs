using Domain.Helpers;
using Infraestructure.Core.Constans;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Handlers;
public class ExtractUserIdFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (string.IsNullOrEmpty(context.HttpContext.Request.Headers["Authorization"]))
        {
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"]!;
            string userId = Helper.GetClaimValue(authorizationHeader, TypeClaims.UserId);
            context.HttpContext.Items["UserId"] = userId; 
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}