namespace Spotitube.Services.Spotify;

public interface ISpotifyService
{
    public Task<IEnumerable<string>> GetAlbumContents(string albumId);

    public Task<IEnumerable<string>> GetPlaylistContents(string playlistId);
}