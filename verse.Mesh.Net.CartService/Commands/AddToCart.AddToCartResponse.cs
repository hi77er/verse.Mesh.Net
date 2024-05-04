namespace verse.Mesh.Net.CartService.Commands;

public record AddToCartResponse(Guid CartId, IEnumerable<Guid> CartItemIds);
