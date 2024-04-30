using Microsoft.AspNetCore.Mvc;

namespace verse.Mesh.Net.CartService.Queries;

public class GetCartByUserRequest
{
  [FromRoute]
  public Guid UserId { get; set; }
}
