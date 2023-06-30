using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ForceNotifyClientsController : ControllerBase
{
    private readonly ILogger<ForceNotifyClientsController> _logger;

    public ForceNotifyClientsController(ILogger<ForceNotifyClientsController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "ClientUpdater")]
    [RequireAuth]
    public async Task<IActionResult> Post()
    {
        await MessageConstructor.DeliverMessageToClients();
        return Ok("Success");

    }
}
