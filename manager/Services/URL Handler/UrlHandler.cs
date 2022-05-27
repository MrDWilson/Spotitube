namespace Spotitube.Services.Url;

using System.Text.RegularExpressions;
using System.Globalization;

public class UrlHandler : IUrlHandler
{
    Regex urlParser = new Regex(@".*\.com\/(\w+)\/(\w+)\?.*",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public(string Id, UrlType UrlType) ParseSpotifyUrl(string url) 
    {
        var matches = urlParser.Match(url);
        if(matches.Groups.Count != 3) {
            throw new Exception("Invalid URL");
        }

        string titleCasedType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(matches.Groups[1].Value);
        UrlType urlType = (UrlType)Enum.Parse(typeof(UrlType), titleCasedType);

        string Id = matches.Groups[2].Value;

        return (Id, urlType);
    }
}