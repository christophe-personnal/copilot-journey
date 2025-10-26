using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using CopilotJourney.Api.Models;

namespace CopilotJourney.Api.Tests;

public class WeatherApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WeatherApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsSuccessAndJson()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task GetWeatherForecast_Returns_Deserializable_Array()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var forecasts = await client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");

        // Assert
        Assert.NotNull(forecasts);
        Assert.NotEmpty(forecasts!);
        foreach (var f in forecasts!)
        {
            Assert.True(f.TemperatureC >= -100 && f.TemperatureC <= 1000, "TemperatureC seems out of expected range");
            Assert.False(string.IsNullOrWhiteSpace(f.Summary));
            Assert.True(f.Date.Year >= 2000, "Date looks invalid");
        }
    }
}
