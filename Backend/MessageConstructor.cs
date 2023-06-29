static class MessageConstructor
{
    async public static Task DeliverMessageToClient(string email) {
        HttpClient client = new();
        EpicGamesApi apiController = new();
        EpicGamesParser epicParser = new();
        EmailSender emailController = new();

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfoModel> currentEpicGames = epicParser.GetCurrentGamesFromEpicRequest(resp);

        emailController.SendEmail(EpicGamesEmailMessageBuilder.BuildEpicGamesMessage(currentEpicGames), email); 
    }
}