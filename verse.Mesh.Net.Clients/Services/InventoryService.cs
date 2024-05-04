using verse.Mesh.Net.Clients.Base;
using verse.Mesh.Net.Clients.Facade;
using verse.Mesh.Net.Models;

namespace verse.Mesh.Net.Clients.Services;
public class InventoryService(WebApiClient webApiClient) 
  : BaseService(webApiClient), IInventoryService
{
  public async Task<ProductRecord?> GetByIdAsync(Guid id)
  {
    var result = await this._webApiClient.GetAsync<ProductRecord>($"/{id}");

    return result;
  }
}
