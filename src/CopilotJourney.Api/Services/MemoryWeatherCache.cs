using System;
using System.Threading.Tasks;
using CopilotJourney.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CopilotJourney.Api.Services;

public class MemoryWeatherCache : IWeatherCache
{
    private readonly IMemoryCache _cache;

    public MemoryWeatherCache(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<T?> GetAsync<T>(string key) where T : class
    {
        _cache.TryGetValue(key, out T? value);
        return Task.FromResult(value);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan ttl) where T : class
    {
        _cache.Set(key, value, ttl);
        return Task.CompletedTask;
    }
}
