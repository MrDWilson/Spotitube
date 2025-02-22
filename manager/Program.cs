using Spotitube.Services.Url;
using Spotitube.Services.Spotify;
using Spotitube.Services.YouTube;
using Spotitube.Services.Converter;
using Spotitube.Services.Playlist;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IUrlHandler, UrlHandler>();
builder.Services.AddScoped<ISpotifyService, SpotifyService>();
builder.Services.AddScoped<IYouTubeService, YouTubeService>();
builder.Services.AddScoped<IConverterService, ConverterService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

string rawPort = Environment.GetEnvironmentVariable("PORT") ?? "80";
if(!int.TryParse(rawPort, out int PORT)) {
    throw new Exception("PORT environment variable must be a number.");
}

app.Run($"http://*:{PORT}");
