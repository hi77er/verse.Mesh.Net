namespace verse.Mesh.Net.InventoryService.Products;

public record ProductRecord(Guid Id, string Name, decimal Price, string? Description = null);
