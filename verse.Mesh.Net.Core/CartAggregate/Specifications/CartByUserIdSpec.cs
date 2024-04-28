using Ardalis.Specification;

namespace verse.Mesh.Net.Core.CartAggregate.Specifications;

public class CartByUserIdSpec : Specification<Cart>
{
  public CartByUserIdSpec(Guid userId)
  {
    Query
        .Where(cart => cart.UserId == userId);
  }
}
