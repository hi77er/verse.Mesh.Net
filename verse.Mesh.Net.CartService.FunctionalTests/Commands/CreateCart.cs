using System.Text;
using System.Text.Json;
using Ardalis.HttpClientTestExtensions;
using verse.Mesh.Net.CartService.Commands;
using verse.Mesh.Net.CartService.FunctionalTests.Base;
using verse.Mesh.Net.Infrastructure.Models;
using verse.Mesh.Net.Infrastructure.Data.Config;

namespace verse.Mesh.Net.CartService.FunctionalTests.Commands;

public class CreateCart : BaseFixture
{
  [SetUp]
  public void Setup() { }

  [Test]
  public async Task ReturnsSuccess()
  {
    var createRoute = "/";
    var seededUserId = Guid.NewGuid();

    var request = new CreateCartRequest(seededUserId, new List<CreateCartItemRecord>());
    var serialized = JsonSerializer.Serialize(request);
    var content = new StringContent(serialized, Encoding.UTF8, AppConstants.MEDIA_TYPE_STRING);

    var createResult = await _client.PostAndDeserializeAsync<CreateCartResponse>(createRoute, content);

    Assert.That(createResult, Is.Not.Null);
    Assert.That(createResult.UserId, Is.Not.Empty);
    Assert.That(createResult.UserId, Is.EqualTo(seededUserId));

    var getRoute = $"/{createResult.UserId}";
    var getResult = await _client.GetAndDeserializeAsync<Models.CartRecord?>(getRoute);

    Assert.That(getResult, Is.Not.Null);
    Assert.That(getResult.UserId, Is.Not.Empty);
    Assert.That(getResult.UserId, Is.EqualTo(seededUserId));
  }

  [Test]
  public async Task GivenNonExisting_ReturnsNotFound()
  {
    var userId = Guid.NewGuid();
    var route = $"/{userId}";

    _ = await _client.GetAndEnsureNotFoundAsync(route);
  }

  [Test]
  public async Task GivenEmpty_BadRequest()
  {
    var createRoute = "/";

    var request = new CreateCartRequest(Guid.Empty, new List<CreateCartItemRecord>());
    var serialized = JsonSerializer.Serialize(request);
    var content = new StringContent(serialized, Encoding.UTF8, AppConstants.MEDIA_TYPE_STRING);

    _ = await _client.PostAndEnsureBadRequestAsync(createRoute, content);
  }
}
