using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers;

[ApiController]
[Route("[controller]")]
public class EpicGamesDBInteractController : ControllerBase
{
    private readonly ILogger<EpicGamesDBInteractController> _logger;
    private IDatabaseIO _dbIO = DataIOFactory.DatabaseIOCreate();

    public EpicGamesDBInteractController(ILogger<EpicGamesDBInteractController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetCurrentEpicGamesInDB", Name = "GetCurrentEpicGamesInDB")]
    public async Task<IActionResult> Get() {
        List<EpicGameInfoModel> data = await _dbIO.RetrieveFromEpicGamesDB();
        
        return Ok(data);
    }

    [HttpPut("ForceUpdateEpicGamesDB", Name = "ForceUpdateEpicGamesDB")]
    [RequireAdminAuth]
    public async Task<IActionResult> Put()
    {

        HttpClient client = new();
        EpicGamesApi apiController = new();
        EpicGamesParser epicParser = new();

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfoModel> currentEpicGames = epicParser.GetCurrentGamesFromEpicRequest(resp);

        _dbIO.WriteEpicGamesDB(currentEpicGames);

        return Ok("Success");

    }
}
