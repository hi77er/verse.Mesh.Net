namespace verse.Mesh.Net.InventoryService.Carts;

public class GetCartByUserRequest
{
  public const string Route = "/carts/{userId}";
  public static string BuildRoute(string userId) => Route.Replace("{userId}", userId);

  public Guid UserId { get; set; }
}
