using Ardalis.Result;
using verse.Mesh.Net.Core.CartAggregate;
using verse.Mesh.Net.Core.Shared;
using verse.Mesh.Net.Infrastructure.Data.MemCache;
using verse.Mesh.Net.UseCases.Products;

namespace verse.Mesh.Net.UseCases.Carts.Get;

public class GetCartByUserHandler(IDistributedCacheAdapter _cacheService) : IQueryHandler<GetCartByUserQuery, Result<CartDTO>>
{
  public async Task<Result<CartDTO>> Handle(GetCartByUserQuery request, CancellationToken cancellationToken)
  {
    // in case we had a repo
    //var spec = new CartByUserIdSpec(request.userId);

    // Because of the specifics of the mem caches we cannot execute  query but rather oly search by key,
    // so for the purpose of the example we'll need to have inserted a record for the userId as a
    // key in the cache
    var entity = await _cacheService.GetItemAsync<Cart>(request.UserId.ToString());
    if (entity == null) return Result.NotFound();

    var itemsDTOs = entity
      .Items
      .Select(item =>
      {
        var product = new ProductDTO(
          item.Product.Id,
          item.Product.Name,
          item.Product.Price,
          item.Product.Description);

        return new CartItemDTO(item.Id, product);
      });

    return new CartDTO(entity.Id, entity.UserId, itemsDTOs);
  }
}
