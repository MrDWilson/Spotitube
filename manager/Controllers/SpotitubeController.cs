using Microsoft.AspNetCore.Mvc;
using Spotitube.Services.Url;

namespace Spotitube.Controllers;

[ApiController]
public class SpotitubeController : ControllerBase
{
    private readonly IUrlHandler _urlHandler;

    public SpotitubeController(IUrlHandler urlHandler)
    {
        _urlHandler = urlHandler;
    }

    [HttpGet("/")]
    public string Get()
    {
        return _urlHandler.ParseSpotifyUrl("url").Id;
    }
}
