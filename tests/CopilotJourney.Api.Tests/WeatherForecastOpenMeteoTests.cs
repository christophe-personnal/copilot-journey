using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CopilotJourney.Application.Dto;
using CopilotJourney.Infrastructure.Services;
using CopilotJourney.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CopilotJourney.Api.Tests;

public class WeatherForecastOpenMeteoTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WeatherForecastOpenMeteoTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetForecast_Returns_7_Days_From_OpenMeteo()
    {
        // Arrange: create a mock HTTP handler returning a minimal OpenMeteo daily response
        var json = @"{
  ""daily"": {
    ""time"": [""2025-10-26"", ""2025-10-27"", ""2025-10-28"", ""2025-10-29"", ""2025-10-30"", ""2025-10-31"", ""2025-11-01""],
    ""temperature_2m_max"": [10, 11, 12, 13, 14, 15, 16],
    ""temperature_2m_min"": [1, 2, 3, 4, 5, 6, 7],
    ""precipitation_sum"": [0, 0.5, 0, 1.2, 0, 0, 0]
  }
}";

        var mockHandler = new SimpleMockHandler(json);

        var factory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Replace the IOpenMeteoClient with one that uses our mock handler
                services.AddSingleton<IOpenMeteoClient>(new OpenMeteoClient(new HttpClient(mockHandler) { BaseAddress = new System.Uri("https://api.open-meteo.com/") }));
            });
        });

        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weather/forecast?lat=48.8566&lon=2.3522");

        // Assert
        response.EnsureSuccessStatusCode();
        var dto = await response.Content.ReadFromJsonAsync<DailyForecastDto>();
        Assert.NotNull(dto);
        Assert.Equal(7, dto!.Days.Count);
    }

    private class SimpleMockHandler : HttpMessageHandler
    {
        private readonly string _json;

        public SimpleMockHandler(string json)
        {
            _json = json;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var resp = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(_json, Encoding.UTF8, "application/json")
            };
            return Task.FromResult(resp);
        }
    }
}
