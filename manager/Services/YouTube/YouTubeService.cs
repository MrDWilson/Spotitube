namespace Spotitube.Services.YouTube;

using Spotitube.Models;

public class YouTubeService : IYouTubeService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public YouTubeService(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    async public Task<string> GetYouTubeLink(string trackId) 
    {
        var httpClient = _httpClientFactory.CreateClient();
        return await GetTrackLink(httpClient, trackId);
    }

    async public Task<IEnumerable<string>> GetYouTubeLinks(IEnumerable<string> trackIds)
    {
        List<string> tracks = new();

        var httpClient = _httpClientFactory.CreateClient();
        foreach(var track in trackIds) 
        {
            tracks.Add(await GetTrackLink(httpClient, track));
        }

        return tracks;
    }

    async private Task<string> GetTrackLink(HttpClient httpClient, string trackId) {
        UrlRequest data = new() { url = trackId };
        var httpResponseMessage = await httpClient.PostAsJsonAsync("http://spotitube_songs", data);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return "https://www.youtube.com/watch?v=" +  await httpResponseMessage.Content.ReadAsStringAsync();
        }
        else
        {
            throw new Exception("Could not retrieve YouTube URL");
        }
    }
}