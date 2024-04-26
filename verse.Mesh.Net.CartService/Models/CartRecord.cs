namespace verse.Mesh.Net.CartService.Models;

public record CartRecord(Guid Id, Guid UserId, IEnumerable<CartItemRecord> CartItems);
