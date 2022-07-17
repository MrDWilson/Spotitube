namespace Spotitube.Services.Spotify;

public interface ISpotifyService
{
    public Task<IEnumerable<string>> GetAlbumContents(string albumId, int start = 1);

    public Task<IEnumerable<string>> GetPlaylistContents(string playlistId, int start = 1);
}