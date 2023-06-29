using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageClientController : ControllerBase
{
    private readonly ILogger<MessageClientController> _logger;

    public MessageClientController(ILogger<MessageClientController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "ClientUpdater")]
    public async Task<IActionResult> Post([FromBody] ClientUpdateModel body)
    {
        var authResult = AuthenticationHelper.AuthenticateRequest(Request);

        if (authResult.GetType() != typeof(OkResult)) {
            return authResult;
        }

        var email = new EmailAddressAttribute();

        if (body is null || body.Email is null) {
            return BadRequest("Request body invalid");
        }
        if (!email.IsValid(body.Email)) {
            return BadRequest("Not a valid Email");
        }

        await MessageConstructor.DeliverMessageToClient(body.Email);
        return Ok("Success");

    }
}
