using verse.Mesh.Net.UseCases.Products;

namespace verse.Mesh.Net.UseCases.Carts;

public record AddToCartItemDTO(Guid ProductId, int Quantity);
