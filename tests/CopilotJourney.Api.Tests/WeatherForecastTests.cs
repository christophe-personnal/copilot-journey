using Xunit;
using CopilotJourney.Api.Models;

namespace CopilotJourney.Api.Tests;

public class WeatherForecastTests
{
    [Fact]
    public void TemperatureF_Is_Calculated_Correctly()
    {
        // Arrange
        var tempC = 0;
        var forecast = new WeatherForecast(System.DateOnly.FromDateTime(System.DateTime.UtcNow), tempC, "Freezing");

        // Act
        var tempF = forecast.TemperatureF;

        // Assert
        Assert.Equal(32, tempF);
    }

    [Theory]
    [InlineData(-40, -40)]
    [InlineData(100, 212)]
    public void TemperatureF_Roughly_Matches_Expected(int c, int expectedF)
    {
        var forecast = new WeatherForecast(System.DateOnly.FromDateTime(System.DateTime.UtcNow), c, "Test");
        // Allow small rounding differences
        Assert.InRange(forecast.TemperatureF, expectedF - 1, expectedF + 1);
    }
}
