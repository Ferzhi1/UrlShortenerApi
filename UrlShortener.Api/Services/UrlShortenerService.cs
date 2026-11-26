using System.Threading;

using System.Collections.Concurrent;

namespace UrlShortener.Api.Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly ConcurrentDictionary<string, string> _urlMap = new();
    private readonly Random _random = new();

    private string GenerateCode(int length = 6)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }

    public string ShortenUrl(string url)
    {
        string code;
        do
        {
            code = GenerateCode(6);
        } while (!_urlMap.TryAdd(code, url));

        return code;
    }

    public string? GetOriginalUrl(string code)
    {
        _urlMap.TryGetValue(code, out var originalUrl);
        return originalUrl;
    }
}