using Microsoft.AspNetCore.Mvc;

static class AuthenticationHelper {
    public static IActionResult AuthenticateRequest(HttpRequest request) {

        string hashedTargetToken = Environment.GetEnvironmentVariable("GAMESREMINDER_AUTH_TOKEN") ?? "";

        if (hashedTargetToken == "") {
            return new BadRequestObjectResult("No internal auth token set");
        }

        if (!request.Headers.TryGetValue("Authorization", out var headerValues)) {
            return new BadRequestObjectResult("No authorization header");
        }

        string strippedHeaderValue = headerValues.ToString().Substring(headerValues.ToString().IndexOf("Bearer ") + "Bearer ".Length);

        if (HashHelper.HashString(strippedHeaderValue) != hashedTargetToken) {
            return new UnauthorizedObjectResult("Invalid token");
        }

        return new OkResult();
    }
}