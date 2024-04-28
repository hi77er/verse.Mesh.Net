﻿using Ardalis.HttpClientTestExtensions;
using verse.Mesh.Net.CartService.Queries;

namespace verse.Mesh.Net.FunctionalTests.CartService;

public class CartServiceTests
{
  private readonly HttpClient _client;

  public CartServiceTests()
  {
    var factory = new CustomWebApplicationFactory<Net.CartService.Program>();

    this._client = factory.CreateClient();
  }

  [SetUp]
  public void Setup() { }

  [Test]
  public async Task GetCartByUser_NonExisting_ReturnsNull()
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
  public async Task ReturnsNotFoundGivenId1000()
  {
    var userId = Guid.NewGuid();
    var route = GetCartByUserRequest.BuildRoute(userId.ToString());

    _ = await _client.GetAndEnsureNotFoundAsync(route);
  }

  [OneTimeTearDown]
  public void Cleanup()
  {
    _client?.Dispose();
  }
}
