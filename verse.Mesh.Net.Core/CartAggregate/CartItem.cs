using System.Net.Http.Headers;
using verse.Mesh.Net.Core.ProductAggregate;
using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.Core.CartAggregate;

public class CartItem(Product product, int quantity) : EntityBase
{
  public Product Product { get; set; } = product;
  public int Quantity { get; set; } = quantity;

}
