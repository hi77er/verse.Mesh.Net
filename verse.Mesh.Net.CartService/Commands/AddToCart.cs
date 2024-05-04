using FluentValidation;
using MediatR;
using verse.Mesh.Net.UseCases.Carts;
using verse.Mesh.Net.UseCases.Carts.Create;

namespace verse.Mesh.Net.CartService.Commands;

/// <summary>
/// Create a new Cart
/// </summary>
/// <remarks>
/// Creates a new Cart given a name.
/// </remarks>
public static class AddToCart
{

  public static WebApplication MapAddToCartEndpoint(this WebApplication app)
  {
    app.MapPost("/", async (AddToCartRequest request, IValidator<AddToCartRequest> validator) =>
    {
      var validationResult = validator.Validate(request);
      if (!validationResult.IsValid)
      {
        return Results.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
      }

      var mediator = app.Services.GetRequiredService<IMediator>();

      var command = new AddToCartCommand(
        request.CartId, 
        request.Items ?? new List<AddToCartItemDTO>());

      var result = await mediator.Send(command);

      if (result.IsSuccess)
      {
        var response = new AddToCartResponse(request.CartId, result.Value);
        return Results.Ok(response);
      }

      return Results.Problem();
    });

    return app;
  }
}
