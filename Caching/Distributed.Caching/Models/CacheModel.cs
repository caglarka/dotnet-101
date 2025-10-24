namespace Distributed.Caching.Models;

public sealed record CacheModel<T>(string Key, T Value) where T : class;