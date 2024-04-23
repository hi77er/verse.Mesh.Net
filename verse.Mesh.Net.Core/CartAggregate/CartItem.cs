using System.Net.Http.Headers;
using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.Core.CartAggregate;

public class CartItem(Product product) : EntityBase
{
  public Product Product { get; set; } = product;

}
