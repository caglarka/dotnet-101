using Application.Interfaces;

namespace Application.Services;

public class InMemoryCachingService(IMemoryCache cache):IInMemoryCachingService
{
    public void Set(string key, string value) => cache.Set(key, value, options: new MemoryCacheEntryOptions
    {
        AbsoluteExpiration = DateTime.Now.AddSeconds(30),
        SlidingExpiration = TimeSpan.FromMinutes(10)
    });

    public string? Get(string key) => cache.Get(key)?.ToString();
}