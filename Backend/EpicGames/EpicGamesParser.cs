using Newtonsoft.Json.Linq;

class EpicGamesParser {
    public List<EpicGameInfoModel> GetCurrentGamesFromEpicRequest(string jsonString) {
        List<EpicGameInfoModel> currentGames = new();

        var json = JObject.Parse(jsonString) ?? throw new Exception("String Not Found");

        JArray elements = json["data"]?["Catalog"]?["searchStore"]?["elements"] as JArray ?? new JArray();
        foreach (JObject game in elements) {
            if (game["promotions"]?.ToString() == "") {
                continue; 
            }
            string title = game["title"]?.ToString() ?? "";

            string? productSlug = null;
            foreach (JObject obj in game["customAttributes"] ?? new JArray())
            {
                if (obj["key"]?.ToString() == "com.epicgames.app.productSlug")
                {
                    productSlug = obj["value"]?.ToString();
                    if (productSlug != null) 
                    {
                        break;
                    }
                }
            }

            string urlRoute = $"store.epicgames.com/en-US/p/{productSlug}";

            var currentPromotionalOffers = game["promotions"]?["promotionalOffers"];

            if ((currentPromotionalOffers as JArray)?.Count != 0) {
                var promoInfo = game["promotions"]?["promotionalOffers"]?[0]?["promotionalOffers"]?[0];
        
                DateTime startDate = DateTime.Parse(promoInfo?["startDate"]?.ToString() ?? "");
                DateTime endDate = DateTime.Parse(promoInfo?["endDate"]?.ToString() ?? "");    

                if (confirmIsGameCurrent(startDate, endDate)) {
                    currentGames.Add(new EpicGameInfoModel(title, urlRoute));
                }        
            }
        }
        return currentGames;
    }
    private static bool confirmIsGameCurrent(DateTime startDate, DateTime endDate) {
        DateTime currentTime = DateTime.UtcNow;
        return startDate < currentTime && endDate >= currentTime;
    }

    private static bool confirmIsGameUpcoming(DateTime startDate, DateTime endDate) {
        DateTime currentTime = DateTime.UtcNow;
        return startDate > currentTime && endDate > currentTime;
    }
}
