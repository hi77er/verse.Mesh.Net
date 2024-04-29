namespace verse.Mesh.Net.CartService.Models;

public record CreateCartRecord(Guid UserId, IEnumerable<CreateCartItemRecord> CartItems);
