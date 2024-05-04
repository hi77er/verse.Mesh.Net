using FluentValidation;
using verse.Mesh.Net.InventoryService.Products;

namespace verse.Mesh.Net.CartService.Commands;

public class GetProductByIdValidator : AbstractValidator<GetProductByIdRequest>
{
  public GetProductByIdValidator()
  {
    RuleFor(x => x.Id)
        .NotEmpty()
        .WithMessage("Please provide a valid product ID");
  }
}
