using Ardalis.Result;
using Ardalis.Specification;
using verse.Mesh.Net.Core.CartAggregate;
using verse.Mesh.Net.Core.ProductAggregate;
using verse.Mesh.Net.Core.Shared;
using verse.Mesh.Net.Infrastructure.Data.MemCache;

namespace verse.Mesh.Net.UseCases.Carts.Create;

public class CreateCartHandler(IDistributedCacheAdapter _cacheService) : ICommandHandler<CreateCartCommand, Result<Guid>>
{
  public async Task<Result<Guid>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
  {
    var itemEntities = request
      .Items
      .Select(item =>
      {
        // TODO:
        // Get the actual product by item.ProductId from the DB/cache here.
        var product = new Product("", 0.0m) { Id = item.ProductId };

        return new CartItem(product, item.Quantity);
      });

    var cartId = Guid.NewGuid();
    var entity = new Cart(request.UserId, itemEntities)
    {
      Id = cartId,
    };

    // Saving it with a key = userId in order to e able to "query" it by userId later
    var success = await _cacheService.SetItemAsync(request.UserId.ToString(), entity);

    return request.UserId;
  }
}
