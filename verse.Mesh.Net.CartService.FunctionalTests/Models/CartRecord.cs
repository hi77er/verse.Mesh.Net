namespace verse.Mesh.Net.CartService.FunctionalTests.Models;

public record CartRecord(Guid Id, Guid UserId, IEnumerable<CartItemRecord> CartItems);
