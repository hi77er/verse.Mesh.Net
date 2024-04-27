using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using verse.Mesh.Net.Infrastructure.Data.Config;
using verse.Mesh.Net.Core.Extensions;

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

  public async Task<bool> SetItemAsync<TItem>(string key, TItem val, int? expiresMin = null)
  {
    Guard.Against.Null(val);
    Guard.Against.Null(this._db);
    var expiresSeconds = (expiresMin ?? this._memCacheExpiresMin) * 60;

    var serializedValue = default(string);
    if (val.IsByteArray())
    {
      throw new NotSupportedException("Byte array not supported for now.");
    }
    else if (val.IsScalar())
    {
      serializedValue = val?.ToString();
    }
    else
    {
      serializedValue = JsonSerializer.Serialize(val);
    }

    var succeeded = await _db
      .StringSetAsync(key, serializedValue, TimeSpan.FromSeconds(expiresSeconds));

    return succeeded;
  }

  public async Task<T?> GetItemAsync<T>(string key)
  {
    Guard.Against.Null(this._db);

    var redisValue = await this._db.StringGetAsync(key);

    T? value = redisValue.HasValue
      ? JsonSerializer.Deserialize<T?>(redisValue.ToString())
      : default(T?);

    return value;
  }


}
