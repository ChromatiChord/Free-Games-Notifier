static class EpicGamesEmailMessageBuilder {
    public static string BuildEpicGamesMessage(List<EpicGameInfo> epicGames) {
        string epicGamesList = "";

        foreach (EpicGameInfo game in epicGames) {
            epicGamesList += $"<li><a href=\"{game.ProductUrl}\">{game.Name}</a></li>";
        }
        
        string returnString = $@"<div>Hi,<br/>
            Here's a list of all the new free epic games currently available:<br/>
            <ul>
            {epicGamesList}
            </ul>
            Regards, Free Games Reminder </div>";

        return returnString;
    }
}