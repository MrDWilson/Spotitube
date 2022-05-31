namespace Spotitube.Models;

public class UrlRequest 
{
    public string? url { get; set; }
    public bool generatePlaylists { get; set; } = false;
}