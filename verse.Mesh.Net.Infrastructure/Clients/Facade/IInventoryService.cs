using verse.Mesh.Net.Infrastructure.Models;

namespace verse.Mesh.Net.Infrastructure.Clients.Facade;
public interface IInventoryService
{
  Task<ProductRecord?> GetByIdAsync(Guid id);
}
