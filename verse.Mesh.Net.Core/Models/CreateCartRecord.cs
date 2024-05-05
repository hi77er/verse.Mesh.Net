namespace verse.Mesh.Net.Core.Models;

public record CreateCartRecord(Guid UserId, IEnumerable<CreateCartItemRecord> CartItems);
