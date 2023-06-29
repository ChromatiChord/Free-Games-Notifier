using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers;

[ApiController]
[Route("[controller]")]
public class DBInteractController : ControllerBase
{
    private readonly ILogger<DBInteractController> _logger;

    public DBInteractController(ILogger<DBInteractController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "SaveItemsToDB")]
    public async Task<IActionResult> Post([FromBody] ClientUpdateModel body)
    {

        HttpClient client = new();
        EpicGamesApi apiController = new();
        EpicGamesParser epicParser = new();
        DatabaseIO databaseController = new();

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfoModel> currentEpicGames = epicParser.GetCurrentGamesFromEpicRequest(resp);

        databaseController.SaveToDB(currentEpicGames);

        return Ok("Success");

    }

    [HttpGet(Name = "GetItemsFromDB")]
    public IActionResult Get() {
        DatabaseIO databaseController = new();
        List<EpicGameInfoModel> data = databaseController.RetrieveFromDB();
        
        return Ok(data);
    }
}
