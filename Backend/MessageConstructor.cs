using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

static class MessageConstructor
{
    async public static Task DeliverMessageToClient(string email) {
        HttpClient client = new();
        EpicGamesApi apiController = new();
        EpicGamesParser epicParserController = new();
        EmailSender emailController = new();

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfoModel> currentEpicGames = epicParserController.GetCurrentGamesFromEpicRequest(resp);

        emailController.SendEmail(EpicGamesEmailMessageBuilder.BuildEpicGamesMessage(currentEpicGames), email); 
    }
}