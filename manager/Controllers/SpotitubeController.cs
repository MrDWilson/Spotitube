using Microsoft.AspNetCore.Mvc;
using Spotitube.Services.Url;
using Spotitube.Models;

namespace Spotitube.Controllers;

[ApiController]
public class SpotitubeController : ControllerBase
{
    private readonly IUrlHandler _urlHandler;

    public SpotitubeController(IUrlHandler urlHandler)
    {
        _urlHandler = urlHandler;
    }

    [HttpPost("/")]
    public string Post([FromBody] UrlRequest request)
    {
        return _urlHandler.ParseSpotifyUrl(request.url).Id;
    }
}
