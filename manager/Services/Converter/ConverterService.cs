namespace Spotitube.Services.Converter;

using Spotitube.Services.Url;
using Spotitube.Services.Spotify;
using Spotitube.Services.YouTube;
using Spotitube.Services.Playlist;

public class ConverterService : IConverterService
{
    private IUrlHandler _urlHandler;
    private ISpotifyService _spotifyService;
    private IYouTubeService _youtubeService;
    private IPlaylistService _playlistService;

    public ConverterService(IUrlHandler urlHandler, 
        ISpotifyService spotifyService, 
        IYouTubeService youtubeService, 
        IPlaylistService playlistService)
    {
        _urlHandler = urlHandler;
        _spotifyService = spotifyService;
        _youtubeService = youtubeService;
        _playlistService = playlistService;
    }

    async public Task<string?> Convert(string url) 
    {
        (string Id, UrlType urlType) = _urlHandler.ParseSpotifyUrl(url);

        if(urlType == UrlType.Track) 
        {
            return await _youtubeService.GetYouTubeLink(Id);
        }
        
        IEnumerable<string> tracks;
        if (urlType == UrlType.Playlist)
        {
            tracks = await _spotifyService.GetPlaylistContents(Id);
        }
        else if(urlType == UrlType.Album)
        {
            tracks = await _spotifyService.GetAlbumContents(Id);
        }
        else return null;

        IEnumerable<string> trackLinks = await _youtubeService.GetYouTubeLinks(tracks);
        string playlistName = await _playlistService.Convert(trackLinks);
        return playlistName;
    }
}