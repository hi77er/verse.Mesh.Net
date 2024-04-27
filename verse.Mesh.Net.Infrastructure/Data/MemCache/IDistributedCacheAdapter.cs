namespace verse.Mesh.Net.Infrastructure.Data.MemCache;

public interface IDistributedCacheAdapter
{
  Task<bool> SetItemAsync<TItem>(string key, TItem val, int? expiresMin = null);

  Task<TItem?> GetItemAsync<TItem>(string key);
}
