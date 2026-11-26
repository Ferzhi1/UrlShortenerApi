using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Services;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/urls")]
public class UrlsController : ControllerBase
{
    private readonly IUrlShortenerService _service;

    public UrlsController(IUrlShortenerService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Shorten([FromBody] UrlRequest request)
    {
        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            return BadRequest("Invalid URL. Only HTTP/HTTPS URLs are allowed.");
        }

        string code = _service.ShortenUrl(request.Url);
        return Created("", new { Code = code, Url = request.Url });
    }

    [HttpGet("{code}")]
    public new IActionResult Redirect(string code)
    {
        var originalUrl = _service.GetOriginalUrl(code);
        if (originalUrl == null)
        {
            return NotFound();
        }

        return Redirect(originalUrl);
    }
}

public class UrlRequest
{
    public string Url { get; set; } = string.Empty;
}