using FluentValidation;

namespace verse.Mesh.Net.CartService.Commands;

public class AddItemToCartValidator : AbstractValidator<AddToCartRequest>
{
  public AddItemToCartValidator()
  {
    RuleFor(x => x.CartId)
      .NotEmpty()
      .WithMessage("CartId cannot be empty.");

    RuleFor(x => x.Items)
      .NotEmpty()
      .WithMessage("Items that are added should contain values.");
  }
}
