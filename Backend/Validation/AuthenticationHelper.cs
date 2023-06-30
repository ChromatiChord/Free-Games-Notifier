using Microsoft.AspNetCore.Mvc;

static class AuthenticationHelper {
    public static IActionResult AuthenticateRequest(HttpRequest request, bool isAdmin) {

        string hashedTargetToken = Environment.GetEnvironmentVariable("GAMESREMINDER_AUTH_TOKEN") ?? "";
        string hashedAdminTargetToken = Environment.GetEnvironmentVariable("GAMESREMINDER_AUTH_ADMIN_TOKEN") ?? "";

        if (hashedTargetToken == "") {
            return new BadRequestObjectResult("No internal auth token set");
        }

        if (!request.Headers.TryGetValue("Authorization", out var headerValues)) {
            return new BadRequestObjectResult("No authorization header");
        }

        string strippedHeaderValue = headerValues.ToString().Substring(headerValues.ToString().IndexOf("Bearer ") + "Bearer ".Length);
        
        if (isAdmin) {
            if (HashHelper.HashString(strippedHeaderValue) != hashedAdminTargetToken) {
                return new UnauthorizedObjectResult("Invalid token or token doesn't have admin permissions");
            }
        } else {
            if (HashHelper.HashString(strippedHeaderValue) != hashedTargetToken || HashHelper.HashString(strippedHeaderValue) != hashedAdminTargetToken) {
                return new UnauthorizedObjectResult("Invalid token");
            }
        }
        
        return new OkResult();
    }
}