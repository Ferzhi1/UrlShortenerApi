namespace UrlShortener.Api.Services;

public interface IUrlShortenerService
{
    string ShortenUrl(string url);
    string? GetOriginalUrl(string code);
}