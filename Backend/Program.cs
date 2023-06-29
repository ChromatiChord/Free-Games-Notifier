using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

class Program
{
    async static Task Main(string[] args)
    {
        HttpClient client = new();
        EpicGamesApi apiController = new();
        EpicGamesParser epicParserController = new();
        EmailClientController emailController = new();

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfo> currentEpicGames = epicParserController.GetCurrentGamesFromEpicRequest(resp);

        emailController.SendEmail(EpicGamesEmailMessageBuilder.BuildEpicGamesMessage(currentEpicGames), "callumward56@gmail.com"); 
    }
}