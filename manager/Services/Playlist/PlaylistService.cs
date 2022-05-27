namespace Spotitube.Services.Playlist;

public class PlaylistService : IPlaylistService
{
    private string GeneratePlaylistName() => 
        Environment.GetEnvironmentVariable("PLAYLIST_HISTORY")?.ToLower() == "true" 
            ? Guid.NewGuid().ToString()
            : "Spotify";

    async public Task<string> Convert(IEnumerable<string> youtubeLinks) 
    {
        string PLAYLIST_NAME = GeneratePlaylistName();
        string playlistName = PLAYLIST_NAME;
        string playlistFile = PLAYLIST_NAME + ".txt";
        string? playlistFolder = Environment.GetEnvironmentVariable("PLAYLIST_FOLDER");
        if(playlistFolder == null)
        {
            throw new Exception("PLAYLIST_FOLDER env variable missing");
        }

        string allLinks = string.Join(Environment.NewLine, youtubeLinks);
        string newPlaylistFile = Path.Join(playlistFolder, playlistFile);
        await File.WriteAllTextAsync(newPlaylistFile, allLinks);

        return "playlist " + playlistName;
    }

    public void ClearPlaylistCache() 
    {
        string? playlistFolder = Environment.GetEnvironmentVariable("PLAYLIST_FOLDER");
        if(playlistFolder == null)
        {
            throw new Exception("PLAYLIST_FOLDER env variable missing");
        }
        Directory.GetFiles(playlistFolder).ToList().ForEach(File.Delete);
    }
}