namespace Spotitube.Services.Url;

public class UrlHandler : IUrlHandler
{
    public(string Id, UrlType UrlType) ParseSpotifyUrl(string url) {
        return ("Test", UrlType.Track);
    }
}