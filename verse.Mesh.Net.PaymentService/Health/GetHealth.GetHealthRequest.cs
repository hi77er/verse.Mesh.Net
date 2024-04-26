namespace verse.Mesh.Net.PaymentService.Health;

public class GetHealthRequest
{
  public const string Route = "/health";
  public static string BuildRoute() => Route;

  public string ServiceName { get; set; } = "Payment Service";
}
