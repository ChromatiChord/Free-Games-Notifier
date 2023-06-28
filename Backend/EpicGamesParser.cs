using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

public struct EpicGameInfo
{
    public string Name { get; }
    public string ProductSlug { get; }

    public EpicGameInfo(string name, string productSlug)
    {
        Name = name;
        ProductSlug = productSlug;
    }
}


class EpicGamesParser {
    public List<EpicGameInfo> GetCurrentGamesFromEpicRequest(string jsonString) {
        List<EpicGameInfo> currentGames = new();

        var json = JObject.Parse(jsonString);

        if (json is null) {
            throw new Exception("String Not Found");
        }

        foreach (JObject game in json["data"]["Catalog"]["searchStore"]["elements"]) {
            if (game["promotions"].ToString() == "") {
                continue; 
            }
            string title = game["title"].ToString();

            string productSlug = null;
            foreach (JObject obj in game["customAttributes"])
            {
                if ((string)obj["key"] == "com.epicgames.app.productSlug")
                {
                    productSlug = (string)obj["value"];
                    break;
                }
            }

            // string urlRoute = $"store.epicgames.com/en-US/p/{productSlug}";

            var currentPromotionalOffers = game["promotions"]["promotionalOffers"];

            if ((currentPromotionalOffers as JArray).Count != 0) {
                var promoInfo = game["promotions"]["promotionalOffers"][0]["promotionalOffers"][0];
        
                DateTime startDate = DateTime.Parse(promoInfo["startDate"].ToString());
                DateTime endDate = DateTime.Parse(promoInfo["endDate"].ToString());    

                if (confirmIsGameCurrent(startDate, endDate)) {
                    currentGames.Add(new EpicGameInfo(title, productSlug));
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