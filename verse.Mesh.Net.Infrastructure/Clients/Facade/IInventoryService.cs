using verse.Mesh.Net.Core.Models;

namespace verse.Mesh.Net.Infrastructure.Clients.Facade;
public interface IInventoryService
{
  Task<ProductRecord?> GetByIdAsync(Guid id);
}
