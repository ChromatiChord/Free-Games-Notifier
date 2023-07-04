using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

public class ValidateEmailAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var body = context.ActionArguments["body"] as UserUpdateRequestModel;

        var email = new EmailAddressAttribute();

        if (body is null || body.Email is null) {
            context.Result = new BadRequestObjectResult("Request body invalid");
            return;
        }

        if (!email.IsValid(body.Email)) {
            context.Result = new BadRequestObjectResult("Not a valid Email");
            return;
        }

        base.OnActionExecuting(context);
    }
}
