using verse.Mesh.Net.Models;

namespace verse.Mesh.Net.Clients.Facade;
public interface IInventoryService
{
  Task<ProductRecord?> GetByIdAsync(Guid id);
}
