namespace verse.Mesh.Net.InventoryService.Health;

public class GetHealthRequest
{
  public const string Route = "/health";
  public static string BuildRoute() => Route;

  public Guid UserId { get; set; }
}
