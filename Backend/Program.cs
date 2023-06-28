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

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfo> currentGames = epicParserController.GetCurrentGamesFromEpicRequest(resp);

        foreach (var game in currentGames) {
            Console.WriteLine(game.Name);
            Console.WriteLine($"store.epicgames.com/en-US/p/{game.ProductSlug}");
        }
    }
}