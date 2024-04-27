using Ardalis.GuardClauses;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using verse.Mesh.Net.Infrastructure.Data.Config;

namespace verse.Mesh.Net.Infrastructure.Data.MemCache;

public class MemoryCacheService : IDistributedCacheAdapter
{
  private readonly IMemoryCache? _memoryCache;
  private readonly int _memCacheExpiresMin;

  public MemoryCacheService(
    IMemoryCache? memoryCache,
    IOptions<RedisOptions> redisOptions)
  {
    Guard.Against.Null(redisOptions);
    this._memCacheExpiresMin = redisOptions.Value.DefaultExpiresMinutes
      ?? AppConstants.MEM_CACHE_DEFAULT_EXPIRES_MIN;

    Guard.Against.Null(memoryCache);
    this._memoryCache = memoryCache;
  }

  public async Task<bool> SetItemAsync<TItem>(string key, TItem val, int? expiresMin = null)
  {
    Guard.Against.Null(val);
    Guard.Against.Null(this._memoryCache);
    var expiresSeconds = (expiresMin ?? this._memCacheExpiresMin) * 60;

    var now = DateTime.UtcNow;
    var unixEpoch = DateTime.UnixEpoch;
    long elapsedFromUnixTimeSeconds = (long)(now - unixEpoch).TotalSeconds;

    var result = this._memoryCache
      .Set(key, val, DateTimeOffset.FromUnixTimeSeconds(elapsedFromUnixTimeSeconds + expiresSeconds));

    var succeeded = result is not null;
    return await Task.FromResult(succeeded);
  }

  public async Task<TItem?> GetItemAsync<TItem>(string key)
  {
    Guard.Against.Null(this._memoryCache);

    var result = this._memoryCache.Get<TItem?>(key);

    return await Task.FromResult(result);
  }

}
