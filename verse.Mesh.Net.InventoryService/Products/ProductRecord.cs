namespace verse.Mesh.Net.InventoryService.Carts;

public record ProductRecord(Guid Id, string Name, decimal Price, string? Description = null);
