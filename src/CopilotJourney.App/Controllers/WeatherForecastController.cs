using Microsoft.AspNetCore.Mvc;
using CopilotJourney.App.Models;

namespace CopilotJourney.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet("current")]
    public ActionResult<WeatherForecast> GetCurrent()
    {
        var rng = new Random();
        var forecast = new WeatherForecast(DateTime.UtcNow, rng.Next(-20, 55), Summaries[rng.Next(Summaries.Length)]);
        return Ok(forecast);
    }

    [HttpGet("forecast")]
    public ActionResult<IEnumerable<WeatherForecast>> Get7Day()
    {
        var rng = new Random();
        var results = Enumerable.Range(1, 7).Select(i =>
            new WeatherForecast(DateTime.UtcNow.Date.AddDays(i), rng.Next(-20, 55), Summaries[rng.Next(Summaries.Length)])
        );
        return Ok(results);
    }

    [HttpGet("alerts")]
    public ActionResult<IEnumerable<string>> GetAlerts()
    {
        // This sample returns a static list of alerts for demonstration.
        var alerts = new List<string>
        {
            "No alerts at this time.",
        };
        return Ok(alerts);
    }
}
