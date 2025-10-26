using System;
using System.Threading.Tasks;
using CopilotJourney.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CopilotJourney.Api.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : ControllerBase
{
    private readonly IOpenMeteoClient _openMeteoClient;
    private readonly IWeatherCache _cache;

    public WeatherController(IOpenMeteoClient openMeteoClient, IWeatherCache cache)
    {
        _openMeteoClient = openMeteoClient;
        _cache = cache;
    }

    [HttpGet("forecast")]
    public async Task<IActionResult> GetForecast([FromQuery] double? lat, [FromQuery] double? lon)
    {
        if (lat == null || lon == null)
            return BadRequest("Please provide both 'lat' and 'lon' query parameters.");

        if (lat < -90 || lat > 90 || lon < -180 || lon > 180)
            return BadRequest("Latitude must be between -90 and 90 and longitude between -180 and 180.");

        var latitude = Math.Round(lat.Value, 6);
        var longitude = Math.Round(lon.Value, 6);
        var cacheKey = $"forecast:{latitude}:{longitude}";

        var cached = await _cache.GetAsync<object>(cacheKey);
        if (cached is not null)
        {
            return Ok(cached);
        }

        var forecast = await _openMeteoClient.GetDailyForecastAsync(latitude, longitude);

        // Cache for 15 minutes
        await _cache.SetAsync(cacheKey, forecast, TimeSpan.FromMinutes(15));

        return Ok(forecast);
    }
}
