using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PingBackendController : ControllerBase
{
    private readonly ILogger<PingBackendController> _logger;

    public PingBackendController(ILogger<PingBackendController> logger)
    {
        _logger = logger;
    }

    [HttpPut(Name = "DatabaseCheckUpdate")]
    public IActionResult Put([FromBody] ClientUpdateModel body)
    {
        EpicGamesUpdateCheck epicCheckController = new();
        epicCheckController.EpicGamesUpdateChecker();
        return Ok();

    }

    private bool areListsEqual(List<EpicGameInfoModel> list1, List<EpicGameInfoModel> list2) {
        return !list1.Except(list2).Any() && !list2.Except(list1).Any();
    }
}
