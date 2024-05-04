namespace verse.Mesh.Net.InventoryService.Products;

public class GetProductByIdRequest
{
  public const string Route = "/{id}";
  public static string BuildRoute(string id) => Route.Replace("{id}", id);

  public Guid Id { get; set; }
}
