using Ardalis.GuardClauses;
using verse.Mesh.Net.Core.CartAggregate;
using verse.Mesh.Net.Core.ProductAggregate;
using verse.Mesh.Net.Infrastructure.Data.MemCache;

namespace verse.Mesh.Net.Infrastructure.Data;

public class RedisDataSeeder
{
  private readonly IDistributedCacheAdapter _cacheService;

  public static readonly Guid UserId1 = new Guid("1455D169-4216-48CA-901C-555A323BDC14");
  public static readonly Guid UserId2 = new Guid("FD641686-433A-4B0E-81AB-22D3B92FB941");
  public static readonly Product Product1 = new("Nike T-Shirt", 79.99m, "T-Shirt from Nike with a Just Do It sign on it.");
  public static readonly Product Product2 = new("Adidas Sneakers", 129.99m, "White sneakers from Adidas. Collection - Summer 2024.");
  public static readonly Product Product3 = new("Brown Shorts", 41.79m, "Simple brown shorts.");
  public static readonly CartItem CartItem1 = new(Product2, 1);
  public static readonly CartItem CartItem2 = new(Product3, 1);
  public static readonly CartItem CartItem3 = new(Product1, 2);
  public static readonly Cart Cart1 = new(UserId1, new List<CartItem> { CartItem1, CartItem2 });
  public static readonly Cart Cart2 = new(UserId2, new List<CartItem> { CartItem3 });

  public RedisDataSeeder(IDistributedCacheAdapter? redisService)
  {
    Guard.Against.Null(redisService);
    this._cacheService = redisService;
  }

  public void Seed()
  {
    Product1.Id = Guid.NewGuid();
    Product2.Id = Guid.NewGuid();
    Product3.Id = Guid.NewGuid();
    CartItem1.Id = Guid.NewGuid();
    CartItem2.Id = Guid.NewGuid();
    CartItem3.Id = Guid.NewGuid();
    Cart1.Id = Guid.NewGuid();
    Cart2.Id = Guid.NewGuid();

    this._cacheService.SetItemAsync(Product1.Id.ToString(), Product1, 10);
    this._cacheService.SetItemAsync(Product2.Id.ToString(), Product2, 10);
    this._cacheService.SetItemAsync(Product3.Id.ToString(), Product3, 10);
    this._cacheService.SetItemAsync(Cart1.UserId.ToString(), Cart1, 10);
    this._cacheService.SetItemAsync(Cart2.UserId.ToString(), Cart2, 10);
  }
}
