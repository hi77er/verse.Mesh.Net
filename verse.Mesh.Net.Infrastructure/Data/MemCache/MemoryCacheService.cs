using Ardalis.GuardClauses;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
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

  public bool SetItem(string key, string val, int? expiresMin = null)
  {
    var expiresSeconds = (expiresMin ?? this._memCacheExpiresMin) * 60;
    Guard.Against.Null(this._memoryCache);

    var result = this._memoryCache
      .Set(key, val, DateTimeOffset.FromUnixTimeSeconds(expiresSeconds));

    var succeeded = !string.IsNullOrEmpty(result);
    return succeeded;
  }
}
