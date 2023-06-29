using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientManagerController : ControllerBase {
    private readonly ILogger<ClientManagerController> _logger;

    public ClientManagerController(ILogger<ClientManagerController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUsers")]
    public IActionResult Get()
    {
        var authResult = AuthenticationHelper.AuthenticateRequest(Request);

        if (authResult.GetType() != typeof(OkResult))
        {
            return authResult;
        }

        return Ok(new List<string> {"callumward56@gmail.com"});
    }

}