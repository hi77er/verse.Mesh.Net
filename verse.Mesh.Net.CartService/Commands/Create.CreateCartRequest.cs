using System.ComponentModel.DataAnnotations;
using verse.Mesh.Net.Core.Models;

namespace verse.Mesh.Net.CartService.Commands;

public record CreateCartRequest([Required] Guid UserId, IEnumerable<CreateCartItemRecord>? Items);
