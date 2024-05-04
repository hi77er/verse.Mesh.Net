namespace verse.Mesh.Net.Models;

public record CartRecord(Guid Id, Guid UserId, IEnumerable<CartItemRecord> CartItems);
