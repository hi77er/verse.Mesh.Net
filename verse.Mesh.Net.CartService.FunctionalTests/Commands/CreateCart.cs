using Ardalis.HttpClientTestExtensions;
using verse.Mesh.Net.CartService.FunctionalTests.Base;
using verse.Mesh.Net.CartService.Queries;

namespace verse.Mesh.Net.CartService.FunctionalTests.Commands;

public class CreateCart : BaseFixture
{
  [SetUp]
  public void Setup() { }

  [Test]
  public async Task GivenExisting_ReturnsSuccess()
  {
    await Task.CompletedTask;
    //var seededUserId = Guid.NewGuid();
    //var route = GetCartByUserRequest.BuildRoute(seededUserId.ToString());
    //var result = await _client.GetAndDeserializeAsync<CartRecord>(route);

    //Assert.That(result, Is.Not.Null);
    //Assert.That(result.Id, Is.Not.Empty);
    //Assert.That(result.UserId, Is.EqualTo(seededUserId));
  }

  [Test]
  public async Task GivenNonExisting_ReturnsNotFound()
  {
    var userId = Guid.NewGuid();
    var route = GetCartByUserRequest.BuildRoute(userId.ToString());

    _ = await _client.GetAndEnsureNotFoundAsync(route);
  }
}
