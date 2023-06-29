using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;

public class EpicGamesApi {

    async public Task<string> MakeRequest(HttpClient client) {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://store-site-backend-static.ak.epicgames.com/freeGamesPromotions"),
            Headers =
            {},
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}
