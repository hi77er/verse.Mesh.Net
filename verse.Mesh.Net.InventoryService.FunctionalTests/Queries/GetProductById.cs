using Ardalis.HttpClientTestExtensions;
using verse.Mesh.Net.Infrastructure.Data;
using verse.Mesh.Net.InventoryService.Products;
using verse.Mesh.Net.InventoryService.FunctionalTests.Base;

namespace verse.Mesh.Net.InventoryService.FuctionalTests.Queries;

public class GetProductById : BaseFixture
{
  [SetUp]
  public void Setup() { }

  [Test]
  public async Task GivenExisting_ReturnsSuccess()
  {
    var product = RedisDataSeeder.Product1;
    var route = $"/{product.Id}";
    var result = await _client.GetAndDeserializeAsync<ProductRecord>(route);

    Assert.That(result, Is.Not.Null);
    Assert.That(result.Id, Is.Not.Empty);
    Assert.That(result.Name, Is.EqualTo(product.Name));
    Assert.That(result.Price, Is.EqualTo(product.Price));
    Assert.That(result.Description, Is.EqualTo(product.Description));
  }

  [Test]
  public async Task GivenNonExisting_ReturnsNotFound()
  {
    var route = $"/{Guid.NewGuid()}";

    _ = await _client.GetAndEnsureNotFoundAsync(route);
  }

  [Test]
  public async Task GivenEmpty_ReturnsBadRequest()
  {
    var route = $"/{Guid.Empty}";

    _ = await _client.GetAndEnsureBadRequestAsync(route);
  }
}
