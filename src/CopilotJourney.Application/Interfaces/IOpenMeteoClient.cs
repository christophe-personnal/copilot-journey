using System.Threading;
using System.Threading.Tasks;
using CopilotJourney.Application.Dto;

namespace CopilotJourney.Application.Interfaces;

public interface IOpenMeteoClient
{
    /// <summary>
    /// Retrieves a daily forecast for the next days for the supplied latitude/longitude.
    /// </summary>
    Task<DailyForecastDto> GetDailyForecastAsync(double latitude, double longitude, CancellationToken cancellationToken = default);
}
