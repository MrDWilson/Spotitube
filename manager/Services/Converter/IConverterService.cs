namespace Spotitube.Services.Converter;
using Spotitube.Models;

public interface IConverterService
{
    public Task<UrlResult?> Convert(string url);
}