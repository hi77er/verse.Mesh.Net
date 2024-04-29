namespace verse.Mesh.Net.UseCases.Carts;

public record CreateCartDTO(Guid UserId, IEnumerable<CreateCartItemDTO> CartItems);
