namespace verse.Mesh.Net.CartService.Models;

public record ProductRecord(Guid Id, string Name, decimal Price, string? Description = null);
