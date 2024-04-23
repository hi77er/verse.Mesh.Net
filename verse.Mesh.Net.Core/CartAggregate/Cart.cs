using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.Core.CartAggregate;

public class Cart : EntityBase, IAggregateRoot
{
  public Guid UserId { get; set; }
  public IEnumerable<CartItem> Items { get; set; } = Enumerable.Empty<CartItem>();
}
