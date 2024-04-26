using Ardalis.Result;
using FastEndpoints;
using MediatR;
using verse.Mesh.Net.UseCases.Carts.Get;
using verse.Mesh.Net.UseCases.Products;
using verse.Mesh.Net.UseCases.Products.Get;

namespace verse.Mesh.Net.InventoryService.Carts;

/// <summary>
/// Get a product by a given id.
/// </summary>
public class GetById(IMediator _mediator) : Endpoint<GetProductByIdRequest, ProductRecord>
{
  public override void Configure()
  {
    Get(GetProductByIdRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    GetProductByIdRequest request,
    CancellationToken cancellationToken)
  {
    var query = new GetProductByIdQuery(request.Id);

    var result = await _mediator.Send(query, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      var response = Map(result);

      Response = response;
    }
  }

  private static ProductRecord Map(Result<ProductDTO> result)
  {
    var productRecord = new ProductRecord(
                result.Value.Id, 
                result.Value.Name, 
                result.Value.Price, 
                result.Value.Description);

    return productRecord;
  }
}
