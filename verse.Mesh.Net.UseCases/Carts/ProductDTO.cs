namespace verse.Mesh.Net.UseCases.Carts;

public record ProductDTO(Guid Id, string Name, decimal Price, string? Description = null);
