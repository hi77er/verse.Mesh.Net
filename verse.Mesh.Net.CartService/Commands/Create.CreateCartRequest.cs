using System.ComponentModel.DataAnnotations;
using verse.Mesh.Net.CartService.Models;

namespace verse.Mesh.Net.CartService.Commands;

public class CreateCartRequest
{
  public const string Route = "/";

  [Required]
  public Guid UserId { get; set; }
  public IEnumerable<CreateCartItemRecord>? Items { get; set; }
}
