using System;
using System.Threading.Tasks;

namespace CopilotJourney.Application.Interfaces;

public interface IWeatherCache
{
    Task<T?> GetAsync<T>(string key) where T : class;
    Task SetAsync<T>(string key, T value, TimeSpan ttl) where T : class;
}
