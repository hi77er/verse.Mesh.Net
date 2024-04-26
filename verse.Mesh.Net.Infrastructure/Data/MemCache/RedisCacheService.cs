using Ardalis.GuardClauses;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using verse.Mesh.Net.Infrastructure.Data.Config;

namespace verse.Mesh.Net.Infrastructure.Data.MemCache;

public class RedisCacheService : IDistributedCacheAdapter
{
  private readonly IDatabase? _db;
  private readonly int _memCacheExpiresMin;

  public RedisCacheService(
    IConnectionMultiplexer? redisConnection,
    IOptions<RedisOptions> redisOptions)
  {
    Guard.Against.Null(redisOptions);
    this._memCacheExpiresMin = redisOptions.Value.DefaultExpiresMinutes
      ?? AppConstants.MEM_CACHE_DEFAULT_EXPIRES_MIN;

    Guard.Against.Null(redisConnection);
    this._db = redisConnection.GetDatabase();
  }

  public bool SetItem(string key, string val, int? expiresMin = null)
  {
    var expiresSeconds = (expiresMin ?? this._memCacheExpiresMin) * 60;
    Guard.Against.Null(this._db);

    var rKey = new RedisKey(key);
    var rVal = new RedisValue(val);

    var succeeded = _db.StringSet(rKey, rVal, TimeSpan.FromSeconds(expiresSeconds));
    return succeeded;
  }
}
