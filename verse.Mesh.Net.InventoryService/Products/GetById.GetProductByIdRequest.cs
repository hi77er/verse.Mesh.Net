namespace verse.Mesh.Net.InventoryService.Carts;

public class GetProductByIdRequest
{
  public const string Route = "/products/{id}";
  public static string BuildRoute(string id) => Route.Replace("{id}", id);

  public Guid Id { get; set; }
}
