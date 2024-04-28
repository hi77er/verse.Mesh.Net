using FastEndpoints;

namespace verse.Mesh.Net.InventoryService.FuctionalTests.ApiEndpoints;

public class InventoryServiceTests
{
  //private readonly HttpClient _client;

  //public InventoryServiceTests(CustomWebApplicationFactory<Net.InventoryService.Program> factory)
  //{
  //  _client = factory.CreateClient();
  //}

  [SetUp]
  public void Setup() { }

  [Test]
  public void Test1()
  {
    Assert.Pass();
  }

  [OneTimeTearDown]
  public void Cleanup()
  {
    //_client?.Dispose();
  }
}
