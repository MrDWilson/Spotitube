namespace Spotitube.Models;

public class UrlResult 
{
    public string? link { get; set; }
    public ICollection<string>? links { get; set; }
    public string? playlist { get; set; }
}