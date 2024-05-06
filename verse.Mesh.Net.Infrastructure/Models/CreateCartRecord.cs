namespace verse.Mesh.Net.Infrastructure.Models;

public record CreateCartRecord(Guid UserId, IEnumerable<CreateCartItemRecord> CartItems);
