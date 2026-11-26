using Xunit;
using UrlShortener.Api.Services;

namespace UrlShortener.Tests;

public class UrlShortenerServiceTests
{
    private readonly IUrlShortenerService _service;

    public UrlShortenerServiceTests()
    {
        _service = new UrlShortenerService();
    }

    [Fact]
    public void ShortenUrl_ValidUrl_ReturnsSixCharacterCode()
    {
        // Arrange
        string validUrl = "https://www.example.com";

        // Act
        string code = _service.ShortenUrl(validUrl);

        // Assert
        Assert.Equal(6, code.Length);
    }

    [Fact]
    public void GetOriginalUrl_ExistingCode_ReturnsOriginalUrl()
    {
        // Arrange
        string originalUrl = "https://www.example.com";
        string code = _service.ShortenUrl(originalUrl);

        // Act
        string? retrievedUrl = _service.GetOriginalUrl(code);

        // Assert
        Assert.Equal(originalUrl, retrievedUrl);
    }
}