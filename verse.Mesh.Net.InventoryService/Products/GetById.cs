using Ardalis.Result;
using FluentValidation;
using MediatR;
using verse.Mesh.Net.UseCases.Products;
using verse.Mesh.Net.UseCases.Products.Get;

namespace verse.Mesh.Net.InventoryService.Products;

/// <summary>
/// Get a product by a given id.
/// </summary>
public static class GetById
{
  public static WebApplication MapGetByUserEndpoint(this WebApplication app)
  {
    app.MapGet("/{id}", async (Guid id, IValidator<GetProductByIdRequest> validator) =>
    {
      var request = new GetProductByIdRequest { Id = id };
      var validationResult = validator.Validate(request);
      if (!validationResult.IsValid)
      {
        return Results.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
      }

      var mediator = app.Services.GetRequiredService<IMediator>();

      var query = new GetProductByIdQuery(id);

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
