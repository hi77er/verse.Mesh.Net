using verse.Mesh.Net.Infrastructure.Clients.Base;
using verse.Mesh.Net.Infrastructure.Clients.Facade;
using verse.Mesh.Net.Core.Models;

namespace verse.Mesh.Net.Infrastructure.Clients.Services;

public class InventoryService(WebApiClient webApiClient) 
  : BaseService(webApiClient), IInventoryService
{
  public async Task<ProductRecord?> GetByIdAsync(Guid id)
  {
    var result = await this._webApiClient.GetAsync<ProductRecord>($"/{id}");

    return result;
  }
}
