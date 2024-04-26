using verse.Mesh.Net.UseCases.Products;

namespace verse.Mesh.Net.UseCases.Carts;

public record CartItemDTO(Guid Id, ProductDTO Product);
