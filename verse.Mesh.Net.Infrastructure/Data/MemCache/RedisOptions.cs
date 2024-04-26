namespace verse.Mesh.Net.Infrastructure.Data.MemCache;
public class RedisOptions
{
  public string? ConnectionString { get; set; }
  public int? DefaultExpiresMinutes { get; set; }

}
