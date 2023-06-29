using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientUpdateController : ControllerBase
{
    private readonly ILogger<ClientUpdateController> _logger;

    public ClientUpdateController(ILogger<ClientUpdateController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "ClientUpdater")]
    public async Task<IActionResult> Post([FromBody] ClientUpdateModel body)
    {
        var email = new EmailAddressAttribute();

        if (body is null || body.Email is null) {
            return BadRequest();
        }
        if (!email.IsValid(body.Email)) {
            return BadRequest();
        }

        await MessageConstructor.DeliverMessageToClient(body.Email);
        return Ok();

    }
}
