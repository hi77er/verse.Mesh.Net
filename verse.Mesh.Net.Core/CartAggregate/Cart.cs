using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.Core.CartAggregate;

public class Cart(Guid userId, IEnumerable<CartItem> items) : EntityBase, IAggregateRoot
{
  public Guid UserId { get; set; } = userId;
  public IEnumerable<CartItem> Items { get; set; } = items;
}
