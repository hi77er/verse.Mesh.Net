using FluentValidation;

namespace verse.Mesh.Net.CartService.Commands;

public class CreateCartValidator : AbstractValidator<CreateCartRequest>
{
  public CreateCartValidator()
  {
    RuleFor(x => x.UserId)
        .NotEmpty()
        .WithMessage("UserId cannot be empty.");
  }
}
