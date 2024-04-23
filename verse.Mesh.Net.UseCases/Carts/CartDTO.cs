namespace verse.Mesh.Net.UseCases.Carts;

public record CartDTO(Guid Id, Guid UserId, IEnumerable<CartItemDTO> CartItems);
