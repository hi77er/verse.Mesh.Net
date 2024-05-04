using Ardalis.Result;
using verse.Mesh.Net.Core.ProductAggregate;
using verse.Mesh.Net.Core.Shared;
using verse.Mesh.Net.Infrastructure.Data.MemCache;

namespace verse.Mesh.Net.UseCases.Products.Get;

public class GetProductByIdHandler(IDistributedCacheAdapter _cacheService) : IQueryHandler<GetProductByIdQuery, Result<ProductDTO>>
{
  public async Task<Result<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
  {
    // in case we had a repo
    //var spec = new ProductByIdSpec(request.Id);

    // Because of the specifics of the mem caches we cannot execute  query but rather oly search by key,
    // so for the purpose of the example we'll need to have inserted a record for the userId as a
    // key in the cache
    var entity = await _cacheService.GetItemAsync<Product>(request.Id.ToString());
    if (entity == null) return Result.NotFound();

    var product = new ProductDTO(
      entity.Id,
      entity.Name,
      entity.Price,
      entity.Description);

    return product;
  }
}
