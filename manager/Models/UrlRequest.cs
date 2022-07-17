namespace Spotitube.Models;

public class UrlRequest 
{
    public string? url { get; set; }
    public int start { get; set; } = 1;
    public bool generatePlaylists { get; set; } = false;
}