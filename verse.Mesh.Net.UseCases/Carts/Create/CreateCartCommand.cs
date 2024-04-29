using Ardalis.Result;
using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.UseCases.Carts.Create;

/// <summary>
/// Create a new Cart.
/// </summary>
/// <param name="UserId"></param>
/// <param name="Items"></param>
public record CreateCartCommand(Guid UserId, IEnumerable<CreateCartItemDTO> Items) : ICommand<Result<Guid>>;
