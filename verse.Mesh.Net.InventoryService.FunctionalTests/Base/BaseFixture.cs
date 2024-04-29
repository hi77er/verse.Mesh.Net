namespace verse.Mesh.Net.InventoryService.FunctionalTests.Base;
public class BaseFixture
{
  protected readonly HttpClient _client;

  public BaseFixture()
  {
    var factory = new CustomWebApplicationFactory<Program>();

    _client = factory.CreateClient();
  }

  [OneTimeTearDown]
  public void Cleanup()
  {
    this._client.Dispose();
  }
}
