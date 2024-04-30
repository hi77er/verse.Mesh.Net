namespace verse.Mesh.Net.CartService.FunctionalTests.Models;

public record CartItemRecord(Guid Id, Guid UserId, IEnumerable<CartItemRecord> CartItems);
