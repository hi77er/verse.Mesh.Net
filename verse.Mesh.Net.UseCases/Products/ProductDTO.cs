namespace verse.Mesh.Net.UseCases.Products;

public record ProductDTO(Guid Id, string Name, decimal Price, string? Description = null);
