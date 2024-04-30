using Ardalis.Result;
using MediatR;
using verse.Mesh.Net.CartService.Models;
using verse.Mesh.Net.UseCases.Carts;
using verse.Mesh.Net.UseCases.Carts.Get;

namespace verse.Mesh.Net.CartService.Queries;

/// <summary>
/// Get a cart by a given userId.
/// </summary>
public static class GetByUser
{
  public static WebApplication MapGetByUserEndpoint(this WebApplication app)
  {
    app.MapGet("/{userId}", async (Guid userId) =>
    {
      var mediator = app.Services.GetRequiredService<IMediator>();

      var query = new GetCartByUserQuery(userId);

      var result = await mediator.Send(query);

      if (result.Status == ResultStatus.NotFound)
      {
        return Results.NotFound();
      }

      if (result.IsSuccess)
      {
        var response = Map(result);

        return Results.Ok(response);
      }

      return Results.Problem();
    });

    return app;
  }

  private static CartRecord Map(Result<CartDTO> result)
  {
    var cartItemRecords = result
      .Value
      .CartItems
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
