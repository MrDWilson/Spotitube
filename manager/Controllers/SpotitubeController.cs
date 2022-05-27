using Microsoft.AspNetCore.Mvc;
using Spotitube.Models;
using Spotitube.Services.Converter;

namespace Spotitube.Controllers;

[ApiController]
public class SpotitubeController : ControllerBase
{
    private IConverterService _converterService;

    public SpotitubeController(IConverterService converterService)
    {
        _converterService = converterService;
    }

    [HttpPost("/")]
    public IActionResult Post([FromBody] UrlRequest request)
    {
        if(request.url == null) 
        {
            return BadRequest("No URL provided");
        }

        return Ok(_converterService.Convert(request.url));
    }
}
