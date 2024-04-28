using Ardalis.Result;
using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.UseCases.Carts.Get;

public record GetCartByUserQuery(Guid UserId) : IQuery<Result<CartDTO>>;
