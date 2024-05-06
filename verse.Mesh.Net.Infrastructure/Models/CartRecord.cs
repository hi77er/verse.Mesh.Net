namespace verse.Mesh.Net.Infrastructure.Models;

public record CartRecord(Guid Id, Guid UserId, IEnumerable<CartItemRecord> CartItems);
