using System.Diagnostics;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Specification;
using verse.Mesh.Net.Infrastructure.Clients.Facade;
using verse.Mesh.Net.Core.CartAggregate;
using verse.Mesh.Net.Core.ProductAggregate;
using verse.Mesh.Net.Core.Shared;
using verse.Mesh.Net.Infrastructure.Data.MemCache;
using verse.Mesh.Net.Infrastructure.Models;

namespace verse.Mesh.Net.UseCases.Carts.Create;

public class AddToCartHandler(
  IDistributedCacheAdapter _cacheService,
  IInventoryService _inventoryService) : ICommandHandler<AddToCartCommand, Result<IEnumerable<Guid>>>
{
  public async Task<Result<IEnumerable<Guid>>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
  {
    Guard.Against.NullOrEmpty(request.CartItems);

    var newItems = new List<CartItem>();

    foreach (var item in request.CartItems)
    {
      var record = await _inventoryService.GetByIdAsync(item.ProductId);
      Guard.Against.Null(record);

      var product = this.MapProduct(record);
      var cartItemId = Guid.NewGuid();

      newItems.Add(new CartItem(product, item.Quantity));
    }

    var cartId = request.CartId.ToString();
    var cart = await _cacheService.GetItemAsync<Cart>(cartId);
    Guard.Against.Null(cart);

    var existing = new List<CartItem>(cart.Items);
    Guard.Against.Null(existing);
    cart.Items = existing.Concat(newItems);

    var success = await _cacheService.SetItemAsync(cartId, cart);

    return new Result<IEnumerable<Guid>>(cart.Items.Select(x => x.Id));
  }

  private Product MapProduct(ProductRecord record)
  {
    var product = new Product(record.Name, record.Price, record.Description);
    product.Id = record.Id;
    return product;
  }
}
