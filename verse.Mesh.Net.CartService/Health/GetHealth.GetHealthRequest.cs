namespace verse.Mesh.Net.CartService.Health;

public class GetHealthRequest
{
  public const string Route = "/health";
  public static string BuildRoute() => Route;

  public string ServiceName { get; set; } = "Cart Service";
}
