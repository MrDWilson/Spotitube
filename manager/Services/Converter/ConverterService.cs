namespace Spotitube.Services.Converter;

using Spotitube.Services.Url;
using Spotitube.Services.Spotify;
using Spotitube.Services.YouTube;
using Spotitube.Services.Playlist;
using Spotitube.Models;

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

    async public Task<UrlResult?> Convert(string url, bool generatePlaylists) 
    {
        (string Id, UrlType urlType) = _urlHandler.ParseSpotifyUrl(url);

        IEnumerable<string> tracks;
        if(urlType == UrlType.Track) 
        {
            return new UrlResult {
                link = await _youtubeService.GetYouTubeLink(Id)
            };
        }
        else if (urlType == UrlType.Playlist)
        {
            tracks = await _spotifyService.GetPlaylistContents(Id);
        }
        else
        {
            tracks = await _spotifyService.GetAlbumContents(Id);
        }

        IEnumerable<string> trackLinks = await _youtubeService.GetYouTubeLinks(tracks);

        if(!generatePlaylists) {
            return new UrlResult {
                links = trackLinks.ToList()
            };
        }

        string playlistName = await _playlistService.Convert(trackLinks);
        return new UrlResult {
            playlist = playlistName
        };
    }
}