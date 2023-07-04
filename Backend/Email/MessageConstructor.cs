static class MessageConstructor
{
    async public static Task DeliverMessageToClients() {
        HttpClient client = new();
        EpicGamesApi apiController = new();
        EpicGamesParser epicParser = new();

        IDatabaseIO dbIO = DataIOFactory.DatabaseIOCreate();


        EmailSender emailController = new();

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfoModel> currentEpicGames = epicParser.GetCurrentGamesFromEpicRequest(resp);
        
        List<string> emails = await dbIO.GetAllUserEmails();

        foreach (string email in emails) {
            emailController.SendEmail(EpicGamesEmailMessageBuilder.BuildEpicGamesMessage(currentEpicGames), email); 
        }

    }
}