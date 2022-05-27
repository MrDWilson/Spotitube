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
    public IActionResult Post([FromBody] UrlRequest request)
    {
        if(request.url == null) 
        {
            return BadRequest("No URL provided");
        }

        return Ok(_urlHandler.ParseSpotifyUrl(request.url).Id);
    }
}
