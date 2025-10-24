namespace InMemory.Caching.Interfaces;

public interface IInMemoryCachingService
{
    void Set(string key, string value);
    string? Get(string key);
}