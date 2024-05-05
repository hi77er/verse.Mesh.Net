namespace verse.Mesh.Net.Core.Models;

public record CartRecord(Guid Id, Guid UserId, IEnumerable<CartItemRecord> CartItems);
