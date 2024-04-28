using Ardalis.GuardClauses;
using verse.Mesh.Net.Core.CartAggregate;
using verse.Mesh.Net.Core.ProductAggregate;
using verse.Mesh.Net.Infrastructure.Data.MemCache;

namespace verse.Mesh.Net.Infrastructure.Data;

public class RedisDataSeeder
{
  private readonly IDistributedCacheAdapter _cacheService;

  private static readonly Guid userId1 = Guid.NewGuid();
  private static readonly Guid userId2 = Guid.NewGuid();
  private static readonly Product product1 = new("Nike T-Shirt", 79.99m, "T-Shirt from Nike with a Just Do It sign on it.");
  private static readonly Product product2 = new("Adidas Sneakers", 129.99m, "White sneakers from Adidas. Collection - Summer 2024.");
  private static readonly Product product3 = new("Brown Shorts", 41.79m, "Simple brown shorts.");
  private static readonly CartItem cartItem1 = new(product2, 1);
  private static readonly CartItem cartItem2 = new(product3, 1);
  private static readonly CartItem cartItem3 = new(product1, 2);
  private static readonly Cart cart1 = new(userId1, new List<CartItem> { cartItem1, cartItem2 });
  private static readonly Cart cart2 = new(userId1, new List<CartItem> { cartItem3 });

  public RedisDataSeeder(IDistributedCacheAdapter? redisService)
  {
    Guard.Against.Null(redisService);
    this._cacheService = redisService;
  }

  public void Seed()
  {
    this._cacheService.SetItemAsync(Guid.NewGuid().ToString(), "Test 123", 10);
  }
}
