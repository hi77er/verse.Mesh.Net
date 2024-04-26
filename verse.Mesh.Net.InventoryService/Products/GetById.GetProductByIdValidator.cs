using FastEndpoints;
using FluentValidation;

namespace verse.Mesh.Net.InventoryService.Carts;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class GetProductByIdValidator : Validator<GetProductByIdRequest>
{
  public GetProductByIdValidator()
  {
    RuleFor(x => x.Id)
     .NotNull()
     .NotEmpty();
  }
}
