namespace Spotitube.Services.Playlist;

public interface IPlaylistService
{
    public Task<string> Convert(IEnumerable<string> youtubeLinks);
}