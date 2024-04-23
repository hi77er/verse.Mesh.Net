using Ardalis.Result;
using FastEndpoints;
using MediatR;
using verse.Mesh.Net.UseCases.Carts;
using verse.Mesh.Net.UseCases.Carts.Get;

namespace verse.Mesh.Net.InventoryService.Carts;

/// <summary>
/// Get a cart by a given userId.
/// </summary>
public class GetByUser(IMediator _mediator) : Endpoint<GetCartByUserRequest, CartRecord>
{
  public override void Configure()
  {
    Get(GetCartByUserRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    GetCartByUserRequest request,
    CancellationToken cancellationToken)
  {
    var query = new GetCartByUserQuery(request.UserId);

    var result = await _mediator.Send(query, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      var response = ExtractResponse(result);

      Response = response;
    }
  }

  private static CartRecord ExtractResponse(Result<CartDTO> result)
  {
    var cartItemRecords = result.Value.CartItems
            .Select(x =>
            {
              var productRecord = new ProductRecord(
                x.Product.Id, x.Product.Name, x.Product.Price, x.Product.Description);

              return new CartItemRecord(x.Id, productRecord);
            });

    var cartRecord = new CartRecord(result.Value.Id, result.Value.UserId, cartItemRecords);

    return cartRecord;
  }
}
