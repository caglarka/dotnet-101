namespace Distributed.Caching.Interfaces;

public interface IDistributedCachingService
{
    Task<string?> GetValueAsync(string key);
    Task<T?> GetGenericValueAsync<T>(string key) where T : class;
    Task SetValueAsync(string key, string value, TimeSpan? expiry = null);
    Task SetGenericValueAsync<T>(string key, T value, TimeSpan? expiry = null) where T : class;
    Task<bool> KeyExistsAsync(string key);
    Task KeyDelete(string key);
    Task SetStringArrayToRedisAsync(string hashKey, string fieldKey, string[] values);
    Task<string[]> GetStringArrayFromRedisAsync(string hashKey, string fieldKey);
    Task RemoveStringArrayAsync(string hashKey, string fieldKey);
    Task BulkRemoveStringArraysAsync(string hashKey, List<int> fieldKeys);
    Task<bool> SetIfNotExistsAsync(string key, TimeSpan expiry);
    Task<bool> AcquireLockAsync(string key, TimeSpan ttl);
    Task<bool> ReleaseLockAsync(string key);
    Task<bool> UpdateTtlAsync(string key, TimeSpan ttl);
    Task<long> StringIncrementAsync(string key);
}