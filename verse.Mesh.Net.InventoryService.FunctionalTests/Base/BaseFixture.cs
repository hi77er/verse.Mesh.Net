using verse.Mesh.Net.Clients;
using verse.Mesh.Net.Clients.Base;
using verse.Mesh.Net.Clients.Facade;

namespace verse.Mesh.Net.InventoryService.FunctionalTests.Base;
public class BaseFixture
{
  const string INVENTORY_SERVICE_BASE_URL = "http://localhost:5239/";

  protected readonly HttpClient _client;
  protected readonly IInventoryService _inventoryService;

  public BaseFixture()
  {
    var webAppFactory = new CustomWebApplicationFactory<Program>();
    _client = webAppFactory.CreateClient();

    var serviceFactory = new ServiceFactory();
    _inventoryService = serviceFactory.CreateInventoryService(INVENTORY_SERVICE_BASE_URL);
  }

  [OneTimeTearDown]
  public void Cleanup()
  {
    this._client.Dispose();
  }
}
