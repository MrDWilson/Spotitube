using Microsoft.AspNetCore.Mvc;
using Spotitube.Models;
using Spotitube.Services.Converter;
using Spotitube.Services.Playlist;

namespace Spotitube.Controllers;

[ApiController]
public class SpotitubeController : ControllerBase
{
    private IConverterService _converterService;
    private IPlaylistService _playlistService;

    public SpotitubeController(IConverterService converterService, IPlaylistService playlistService)
    {
        _converterService = converterService;
        _playlistService = playlistService;
    }

    [HttpPost("/")]
    async public Task<IActionResult> Post([FromBody] UrlRequest request)
    {
        if(request.url == null) 
        {
            return BadRequest("No URL provided");
        }

        return Ok(await _converterService.Convert(request.url));
    }

    [HttpGet("/clear")]
    public IActionResult Get() 
    {
        _playlistService.ClearPlaylistCache();
        return Ok();
    }
}
