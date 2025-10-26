using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CopilotJourney.Application.Dto;
using CopilotJourney.Application.Interfaces;

namespace CopilotJourney.Infrastructure.Services;

public class OpenMeteoClient : IOpenMeteoClient
{
    private readonly HttpClient _httpClient;

    public OpenMeteoClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<DailyForecastDto> GetDailyForecastAsync(double latitude, double longitude, CancellationToken cancellationToken = default)
    {
        // Build query for 7-day daily forecast
        var url = $"v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,precipitation_sum&forecast_days=7&timezone=UTC";

        var resp = await _httpClient.GetFromJsonAsync<OpenMeteoDailyResponse?>(url, cancellationToken: cancellationToken).ConfigureAwait(false);
        if (resp == null || resp.Daily == null)
        {
            return new DailyForecastDto(Array.Empty<DailyForecastDayDto>());
        }

        var times = resp.Daily.Time ?? Array.Empty<string>();
        var mins = resp.Daily.Temperature2mMin ?? Array.Empty<double?>();
        var maxs = resp.Daily.Temperature2mMax ?? Array.Empty<double?>();
        var prec = resp.Daily.PrecipitationSum ?? Array.Empty<double?>();

        var days = times.Select((t, i) => new DailyForecastDayDto(
            t,
            i < mins.Length ? mins[i] : null,
            i < maxs.Length ? maxs[i] : null,
            i < prec.Length ? prec[i] : null
        )).ToArray();

        return new DailyForecastDto(days);
    }

    // Minimal response types matching the fields we need
    private sealed class OpenMeteoDailyResponse
    {
        public Daily? Daily { get; set; }
    }

    private sealed class Daily
    {
        public string[]? Time { get; set; }
        public double?[]? Temperature2mMax { get; set; }
        public double?[]? Temperature2mMin { get; set; }
        public double?[]? PrecipitationSum { get; set; }
    }
}
