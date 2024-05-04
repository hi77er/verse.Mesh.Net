using System.ComponentModel.DataAnnotations;
using verse.Mesh.Net.Models;
using verse.Mesh.Net.UseCases.Carts;

namespace verse.Mesh.Net.CartService.Commands;

public record AddToCartRequest([Required] Guid CartId, IEnumerable<AddToCartItemDTO>? Items);
