namespace verse.Mesh.Net.Infrastructure.Data.MemCache;

public interface IDistributedCacheAdapter
{
  bool SetItem(string key, string val, int? expiresMin = null);
}
