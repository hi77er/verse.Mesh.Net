using System.Text.Json;
using Ardalis.HttpClientTestExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using verse.Mesh.Net.CartService.FunctionalTests.Base;
using verse.Mesh.Net.CartService.FunctionalTests.Models;
using verse.Mesh.Net.CartService.Queries;
using verse.Mesh.Net.Infrastructure.Data;

namespace verse.Mesh.Net.CartService.FuctionalTests.Queries;

public class GetCartByUser : BaseFixture
{
  [SetUp]
  public void Setup() { }

  [Test]
  public async Task GivenExisting_ReturnsSuccess()
  {
    var userId = RedisDataSeeder.UserId1.ToString();
    var route = $"/{userId}";
    var result = await _client.GetAndDeserializeAsync<CartRecord>(route);

    Assert.That(result, Is.Not.Null);
    Assert.That(result.Id, Is.Not.Empty);
    Assert.That(result.UserId.ToString(), Is.EqualTo(userId));
  }

  [Test]
  public async Task GivenNonExisting_ReturnsNotFound()
  {
    var userId = Guid.NewGuid();
    var route = $"/{userId}";

    _ = await _client.GetAndEnsureNotFoundAsync(route);
  }
}
