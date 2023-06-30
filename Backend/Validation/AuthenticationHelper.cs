using Microsoft.AspNetCore.Mvc;

static class AuthenticationHelper {
    public static IActionResult AuthenticateRequest(HttpRequest request, bool isAdmin) {

        string tokenTarget = isAdmin ? "GAMESREMINDER_AUTH_ADMIN_TOKEN" : "GAMESREMINDER_AUTH_TOKEN";
        string hashedTargetToken = Environment.GetEnvironmentVariable(tokenTarget) ?? "";

        if (hashedTargetToken == "") {
            return new BadRequestObjectResult("No internal auth token set");
        }

        if (!request.Headers.TryGetValue("Authorization", out var headerValues)) {
            return new BadRequestObjectResult("No authorization header");
        }

        string strippedHeaderValue = headerValues.ToString().Substring(headerValues.ToString().IndexOf("Bearer ") + "Bearer ".Length);

        if (HashHelper.HashString(strippedHeaderValue) != hashedTargetToken) {
            string invalidTokenMessage = isAdmin ? "Invalid token or token doesn't have admin permissions" : "Invalid token";
            return new UnauthorizedObjectResult(invalidTokenMessage);
        }

        return new OkResult();
    }
}