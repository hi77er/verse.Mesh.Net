using StackExchange.Redis;

namespace verse.Mesh.Net.Infrastructure.Data.MemCache;

public interface IRedisService
{
  IDatabase DB { get; }
}
