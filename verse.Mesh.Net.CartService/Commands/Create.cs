using FastEndpoints;
using MediatR;
using verse.Mesh.Net.CartService.Models;
using verse.Mesh.Net.UseCases.Carts;
using verse.Mesh.Net.UseCases.Carts.Create;
using verse.Mesh.Net.UseCases.Products;

namespace verse.Mesh.Net.CartService.Commands;

/// <summary>
/// Create a new Cart
/// </summary>
/// <remarks>
/// Creates a new Cart given a name.
/// </remarks>
public class Create(IMediator _mediator)
  : Endpoint<CreateCartRequest, CreateCartResponse>
{
  public override void Configure()
  {
    Post(CreateCartRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      // XML Docs are used by default but are overridden by these properties:
      //s.Summary = "Create a new Cart.";
      //s.Description = "Create a new Cart. A valid UserId is required.";
      s.ExampleRequest = new CreateCartRequest()
      {
        UserId = Guid.NewGuid(),
        Items = new List<CreateCartItemRecord>()
      };
    });
  }

  public override async Task HandleAsync(
    CreateCartRequest request,
    CancellationToken cancellationToken)
  {
    var cartItemDtos = request
      .Items?
      .Select(item => new CreateCartItemDTO(item.ProductId, item.Quantity))
        ?? new List<CreateCartItemDTO>();

    var command = new CreateCartCommand(request.UserId, cartItemDtos);

    var result = await _mediator.Send(command, cancellationToken);

    if (result.IsSuccess)
    {
      Response = new CreateCartResponse(result.Value);
      return;
    }
  }
}
