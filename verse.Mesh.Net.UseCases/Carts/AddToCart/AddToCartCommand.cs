using Ardalis.Result;
using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.UseCases.Carts.Create;

/// <summary>
/// Get poducts by their Ids and add them to the cart that is found by its Id.
/// </summary>
/// <param name="CartId"></param>
/// <param name="ProductIds"></param>
public record AddToCartCommand(Guid CartId, IEnumerable<AddToCartItemDTO> CartItems) : ICommand<Result<IEnumerable<Guid>>>;
