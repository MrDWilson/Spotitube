namespace Spotitube.Services.Url;

public interface IUrlHandler
{
    public (string Id, UrlType UrlType) ParseSpotifyUrl(string url);
}