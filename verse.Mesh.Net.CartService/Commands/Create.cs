using Ardalis.Result;
using MediatR;
using verse.Mesh.Net.CartService.Models;
using verse.Mesh.Net.UseCases.Carts;
using verse.Mesh.Net.UseCases.Carts.Create;

namespace verse.Mesh.Net.CartService.Commands;

/// <summary>
/// Create a new Cart
/// </summary>
/// <remarks>
/// Creates a new Cart given a name.
/// </remarks>
public static class Create
{

  public static WebApplication MapCreateCartEndpoint(this WebApplication app)
  {
    app.MapPost("/", async (CreateCartRequest request) =>
    {
      var mediator = app.Services.GetRequiredService<IMediator>();

      var cartItemDtos = request
        .Items?
        .Select(item => new CreateCartItemDTO(item.ProductId, item.Quantity))
          ?? new List<CreateCartItemDTO>();

      var command = new CreateCartCommand(request.UserId, cartItemDtos);

      var result = await mediator.Send(command);

      if (result.IsSuccess)
      {
        var response = new CreateCartResponse(result.Value);
        return Results.Ok(response);
      }

      return Results.Problem();
    });

    return app;
  }
}
