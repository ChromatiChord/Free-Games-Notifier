using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

public class RequireAuthAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var authResult = AuthenticationHelper.AuthenticateRequest(context.HttpContext.Request);

        if (authResult.GetType() != typeof(OkResult)) {
            context.Result = authResult;
        }
    }
}
