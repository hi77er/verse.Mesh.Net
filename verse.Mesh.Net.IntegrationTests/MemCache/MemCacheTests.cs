using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;
using FastEndpoints;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using verse.Mesh.Net.Core.ProductAggregate;
using verse.Mesh.Net.Infrastructure.Data.Config;
using verse.Mesh.Net.Infrastructure.Data.MemCache;

namespace verse.Mesh.Net.IntegrationTests.MemCache;

//[TestFixture("Peter")] => ctor => MemCacheTests(string name)
public class MemCacheTests
{
  private readonly IDistributedCacheAdapter _cacheService;

  public MemCacheTests()
  {
    this._cacheService = this.ConfigureCache();
  }

  [SetUp]
  public void Setup()
  {
  }

  //[Ignore("Skipping this test")]
  [Test, TestCaseSource(nameof(GetTestData))]
  public async Task Cache_Add_StringValue_ReturnsSuccess(string key, object value)
  {
    var success = await this._cacheService.SetItemAsync(key, value);
    var found = await this._cacheService.GetItemAsync<object>(key);

    Assert.IsTrue(success);
    Assert.IsNotNull(found);
    Assert.That(found, Is.TypeOf(value.GetType()));
    Assert.That(found, Is.EqualTo(value));
  }

  public static IEnumerable<TestCaseData> GetTestData()
  {
    yield return new TestCaseData("Key1", "Value1");
    yield return new TestCaseData("Key2", 200);
    yield return new TestCaseData("Key2", new Product("Product 1", 2.5m, "Product Description"));
    //yield return new TestCaseData("Key3", new List<string> { "subVal1", "subVal2" }).Returns(3);
  }

  protected IDistributedCacheAdapter ConfigureCache()
  {
    var cacheService = default(IDistributedCacheAdapter?);
    var redisOptionsAccessor = Options.Create(new RedisOptions() { DefaultExpiresMinutes = 1440 });

    var memCache = new MemoryCache(new MemoryCacheOptions());

    cacheService = new MemoryCacheService(memCache, redisOptionsAccessor);

    // INFO: Replace with this once we have a real Redis Cache in the Development environment.
    //var redisOptions = redisOptionsAccessor.Value;
    //var connStr = redisOptions?.ConnectionString ?? string.Empty;
    //var multiplexor = ConnectionMultiplexer.Connect(connStr);
    //cacheService = new RedisCacheService(multiplexor, redisOptionsAccessor);

    return cacheService;
  }
}
