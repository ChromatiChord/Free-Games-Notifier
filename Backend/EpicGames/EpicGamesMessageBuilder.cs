static class EpicGamesEmailMessageBuilder {
    public static string BuildEpicGamesMessage(List<EpicGameInfoModel> epicGames) {
        string epicGamesList = "";
        string openingLine = epicGames.Count == 1 ? "There's a new free Epic Game for you to try! Here it is:<br/>" : "There are some new free Epic Games for you to try! Here they are:<br/>";

        foreach (EpicGameInfoModel game in epicGames) {
            epicGamesList += $"<li><a href=\"{game.ProductUrl}\">{game.Name}</a></li>";
        }
        
        string returnString = $@"<div>Hi,<br/>
            {openingLine}
            <ul>
            {epicGamesList}
            </ul>
            Regards, Free Games Reminder </div>";

        return returnString;
    }
}