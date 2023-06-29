class EpicGamesUpdateCheck {
    async public void EpicGamesUpdateChecker() {
        HttpClient client = new();
        EpicGamesApi apiController = new();
        EpicGamesParser epicParser = new();
        DatabaseIO databaseController = new();

        var resp = await apiController.MakeRequest(client);
        List<EpicGameInfoModel> currentEpicGames = epicParser.GetCurrentGamesFromEpicRequest(resp);

        List<EpicGameInfoModel> storedEpicGames = databaseController.RetrieveFromDB();

        if (!areListsEqual(currentEpicGames, storedEpicGames)) {
            await MessageConstructor.DeliverMessageToClient("callumward56@gmail.com");
            databaseController.SaveToDB(currentEpicGames);
        } 
    }

    private bool areListsEqual(List<EpicGameInfoModel> list1, List<EpicGameInfoModel> list2) {
        return !list1.Except(list2).Any() && !list2.Except(list1).Any();
    }
}