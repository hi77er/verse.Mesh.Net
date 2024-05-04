using verse.Mesh.Net.Clients.Facade;
using verse.Mesh.Net.Clients.Services;

namespace verse.Mesh.Net.Clients.Base;
public class ServiceFactory
{
  public IInventoryService CreateInventoryService(string baseUrl)
  {
    var webApiClient = new WebApiClient(baseUrl);
    var inventoryService = new InventoryService(webApiClient);
    return inventoryService;
  }
}
