using Ardalis.Result;
using Ardalis.Specification;
using verse.Mesh.Net.Core.CartAggregate;
using verse.Mesh.Net.Core.ProductAggregate;
using verse.Mesh.Net.Core.Shared;
using verse.Mesh.Net.Infrastructure.Data.MemCache;
using verse.Mesh.Net.UseCases.Carts.Get;
using verse.Mesh.Net.UseCases.Products;

namespace verse.Mesh.Net.UseCases.Carts.Create;

public class CreateCartHandler(IDistributedCacheAdapter _cacheService) : ICommandHandler<CreateCartCommand, Result<Guid>>
{
  public async Task<Result<Guid>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
  {
    var itemEntities = request
      .Items
      .Select(item =>
      {
        var product = new Product(
          item.Product.Name,
          item.Product.Price,
          item.Product.Description)
        {
          Id = item.Product.Id,
        };

        return new CartItem(product, 2);
      });

    var cartId = Guid.NewGuid();
    var entity = new Cart(request.UserId, itemEntities)
    {
      Id = cartId,
    };
    
    // Saving it with a key = userId in order to e able to "query" it by userId later
    var success = await _cacheService.SetItemAsync<Cart>(request.UserId.ToString(), entity);

    return cartId;
  }
}
