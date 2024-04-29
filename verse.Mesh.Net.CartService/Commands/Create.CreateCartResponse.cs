namespace verse.Mesh.Net.CartService.Commands;

public class CreateCartResponse(Guid Id)
{
  public Guid Id { get; set; } = Id;
}
