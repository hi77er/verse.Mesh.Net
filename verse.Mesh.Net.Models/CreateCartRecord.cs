namespace verse.Mesh.Net.Models;

public record CreateCartRecord(Guid UserId, IEnumerable<CreateCartItemRecord> CartItems);
