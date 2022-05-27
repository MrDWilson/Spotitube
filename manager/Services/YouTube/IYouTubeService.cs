namespace Spotitube.Services.YouTube;

public interface IYouTubeService
{
    public Task<string> GetYouTubeLink(string trackId);

    public Task<IEnumerable<string>> GetYouTubeLinks(IEnumerable<string> trackIds);
}