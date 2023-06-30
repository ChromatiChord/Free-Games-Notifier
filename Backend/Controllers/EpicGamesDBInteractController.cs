using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers;

[ApiController]
[Route("[controller]")]
public class EpicGamesDBInteractController : ControllerBase
{
    private readonly ILogger<EpicGamesDBInteractController> _logger;

    public EpicGamesDBInteractController(ILogger<EpicGamesDBInteractController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetCurrentEpicGamesInDB", Name = "GetCurrentEpicGamesInDB")]
    public IActionResult Get() {
        DatabaseIO databaseController = new();
        List<EpicGameInfoModel> data = databaseController.RetrieveFromEpicGamesDB();
        
        return Ok(data);
    }

    [HttpPut("ForceUpdateEpicGamesDB", Name = "ForceUpdateEpicGamesDB")]
    [RequireAdminAuth]
    public async Task<IActionResult> Put()
    {

        HttpClient client = new();
        EpicGamesApi apiController = new();
        EpicGamesParser epicParser = new();
        DatabaseIO databaseController = new();

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfoModel> currentEpicGames = epicParser.GetCurrentGamesFromEpicRequest(resp);

        databaseController.WriteEpicGamesDB(currentEpicGames);

        return Ok("Success");

    }
}
