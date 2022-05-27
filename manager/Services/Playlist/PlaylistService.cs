namespace Spotitube.Services.Playlist;

public class PlaylistService : IPlaylistService
{
    async public Task<string> Convert(IEnumerable<string> youtubeLinks) 
    {
        string playlistName = Guid.NewGuid().ToString();
        string playlistFile = playlistName + ".txt";
        string? playlistFolder = Environment.GetEnvironmentVariable("PLAYLIST_FOLDER");
        if(playlistFolder == null)
        {
            throw new Exception("PLAYLIST_FOLDER env variable missing");
        }

        string allLinks = string.Join(Environment.NewLine, youtubeLinks);
        string newPlaylistFile = Path.Join(playlistFolder, playlistFile);
        await File.WriteAllTextAsync(newPlaylistFile, allLinks);

        return playlistName;
    }
}