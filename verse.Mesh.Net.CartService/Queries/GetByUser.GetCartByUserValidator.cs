using FastEndpoints;
using FluentValidation;

namespace verse.Mesh.Net.CartService.Queries;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class GetCartByUserValidator : Validator<GetCartByUserRequest>
{
  public GetCartByUserValidator()
  {
    RuleFor(x => x.UserId)
     .NotNull()
     .NotEmpty();
  }
}
