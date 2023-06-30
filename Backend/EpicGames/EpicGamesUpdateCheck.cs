class EpicGamesUpdateCheck {
    async public void EpicGamesUpdateChecker() {
        HttpClient client = new();
        EpicGamesApi apiController = new();
        EpicGamesParser epicParser = new();
        DatabaseIO databaseController = new();

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfoModel> currentEpicGames = epicParser.GetCurrentGamesFromEpicRequest(resp);

        List<EpicGameInfoModel> storedEpicGames = databaseController.RetrieveFromEpicGamesDB();

        if (!areListsEqual(currentEpicGames, storedEpicGames)) {
            await MessageConstructor.DeliverMessageToClients();
            databaseController.WriteEpicGamesDB(currentEpicGames);
        } 
    }

    private bool areListsEqual(List<EpicGameInfoModel> list1, List<EpicGameInfoModel> list2) {
        return !list1.Except(list2).Any() && !list2.Except(list1).Any();
    }
}