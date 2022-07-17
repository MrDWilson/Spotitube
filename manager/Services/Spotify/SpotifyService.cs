namespace Spotitube.Services.Spotify;

using SpotifyAPI.Web;

public class SpotifyService : ISpotifyService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly int MAX_TRACK_COUNT;

    public SpotifyService(IHttpClientFactory httpClientFactory) 
    {
        _httpClientFactory = httpClientFactory;

        var trackCount = Environment.GetEnvironmentVariable("MAX_TRACK_COUNT") ?? "50";
        if(!int.TryParse(trackCount, out MAX_TRACK_COUNT))
        {
            throw new Exception("MAX_TRACK_COUNT environment variable must be a number.");
        }
    }

    async public Task<IEnumerable<string>> GetAlbumContents(string albumId, int start) 
    {
        var spotify = new SpotifyClient(await GetAccessToken());

        var album = await spotify.Albums.Get(albumId);

        List<string> trackIds = new();
        await foreach(var track in spotify.Paginate(album.Tracks)) 
        {
            trackIds.Add(track.Id);
        }

        return trackIds;
    }

    async public Task<IEnumerable<string>> GetPlaylistContents(string playlistId, int start) 
    {
        var spotify = new SpotifyClient(await GetAccessToken());

        var req = new PlaylistGetItemsRequest();
        req.Offset = start - 1;
        var playlist = await spotify.Playlists.GetItems(playlistId, req);

        List<string> trackIds = new();
        if(playlist == null) {
            return new List<string>();
        }

        await foreach(var track in spotify.Paginate(playlist)) 
        {
            if(trackIds.Count == MAX_TRACK_COUNT) 
            {
                break;
            }

            if(track.Track is FullTrack fullTrack)
            {
                trackIds.Add(fullTrack.Id);
            }
        }

        return trackIds;
    }

    async private Task<string> GetAccessToken() 
    {
        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.GetAsync("http://spotitube_token/");

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        else
        {
            throw new Exception("Could not retrieve Spotify token");
        }
    }
}