namespace Spotitube.Services.Converter;

public interface IConverterService
{
    public Task<string?> Convert(string url);
}