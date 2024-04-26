using Ardalis.GuardClauses;
using StackExchange.Redis;

namespace verse.Mesh.Net.Infrastructure.Data.MemCache;

public class RedisService : IRedisService
{
  public IDatabase DB { get; }

  public RedisService(IConnectionMultiplexer redisConnection)
  {
    Guard.Against.Null(redisConnection);

    this.DB = redisConnection.GetDatabase();
  }
}
