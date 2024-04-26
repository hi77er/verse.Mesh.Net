namespace verse.Mesh.Net.CartService.Queries;

public class GetCartByUserRequest
{
  public const string Route = "/{userId}";
  public static string BuildRoute(string userId) => Route.Replace("{userId}", userId);

  public Guid UserId { get; set; }
}
