static class EpicGamesEmailMessageBuilder {
    public static string BuildEpicGamesMessage(List<EpicGameInfoModel> epicGames) {
        string epicGamesList = "";

        foreach (EpicGameInfoModel game in epicGames) {
            epicGamesList += $"<li><a href=\"{game.ProductUrl}\">{game.Name}</a></li>";
        }
        
        string returnString = $@"<div>Hi,<br/>
            There are some new free Epic Games for you to try! Here they are:<br/>
            <ul>
            {epicGamesList}
            </ul>
            Regards, Free Games Reminder </div>";

        return returnString;
    }
}