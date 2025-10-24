using System.Text.Json;
using Distributed.Caching.Interfaces;
using Distributed.Caching.Models.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Distributed.Caching.Services;

public class DistributedCachingService(IConnectionMultiplexer multiplexer, IOptions<RedisOptions> redisOptions)
    : IDistributedCachingService
{
    private readonly IDatabase _db = multiplexer.GetDatabase(redisOptions.Value.Database);
    private readonly string _lockToken = Guid.NewGuid().ToString();

    public async Task<string?> GetValueAsync(string key)
        => await _db.StringGetAsync(key);

    public async Task<T?> GetGenericValueAsync<T>(string key) where T : class
    {
        var json = await _db.StringGetAsync(key);
        return json.HasValue ? JsonSerializer.Deserialize<T>(json!) : null;
    }

    public Task SetValueAsync(string key, string value, TimeSpan? expiry = null)
        => _db.StringSetAsync(key, value, expiry);

    public Task SetGenericValueAsync<T>(string key, T value, TimeSpan? expiry = null) where T : class
    {
        var json = JsonSerializer.Serialize(value);
        return _db.StringSetAsync(key, json, expiry);
    }

    public Task<bool> KeyExistsAsync(string key) => _db.KeyExistsAsync(key);

    public Task KeyDelete(string key) => _db.KeyDeleteAsync(key);

    public Task SetStringArrayToRedisAsync(string hashKey, string fieldKey, string[] values)
    {
        var json = JsonSerializer.Serialize(values);
        return _db.HashSetAsync(hashKey, [new HashEntry(fieldKey, json)]);
    }

    public async Task<string[]> GetStringArrayFromRedisAsync(string hashKey, string fieldKey)
    {
        var val = await _db.HashGetAsync(hashKey, fieldKey);

        if (!val.HasValue)
        {
            return [];
        }

        return JsonSerializer.Deserialize<string[]>(val!)!;
    }

    public Task RemoveStringArrayAsync(string hashKey, string fieldKey) => _db.HashDeleteAsync(hashKey, fieldKey);

    public Task BulkRemoveStringArraysAsync(string hashKey, List<int> fieldKeys)
    {
        var fields = fieldKeys.Select(i => (RedisValue)i.ToString()).ToArray();
        return _db.HashDeleteAsync(hashKey, fields);
    }

    public Task<bool> SetIfNotExistsAsync(string key, TimeSpan expiry)
        => _db.StringSetAsync(key, string.Empty, expiry, when: When.NotExists);

    public Task<bool> AcquireLockAsync(string key, TimeSpan ttl) => _db.LockTakeAsync(key, _lockToken, ttl);

    public Task<bool> ReleaseLockAsync(string key) => _db.LockReleaseAsync(key, _lockToken);

    public Task<bool> UpdateTtlAsync(string key, TimeSpan ttl) => _db.KeyExpireAsync(key, ttl);

    public async Task<long> StringIncrementAsync(string key)
    {
        var newVal = await _db.StringIncrementAsync(key).ConfigureAwait(false);
        return newVal;
    }
}