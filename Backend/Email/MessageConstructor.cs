static class MessageConstructor
{
    async public static Task DeliverMessageToClients() {
        HttpClient client = new();
        EpicGamesApi apiController = new();
        EpicGamesParser epicParser = new();

        DatabaseIO databaseObj = new();

        EmailSender emailController = new();

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfoModel> currentEpicGames = epicParser.GetCurrentGamesFromEpicRequest(resp);
        
        List<string> emails = databaseObj.GetEmails();

        foreach (string email in emails) {
            emailController.SendEmail(EpicGamesEmailMessageBuilder.BuildEpicGamesMessage(currentEpicGames), email); 
        }

    }
}