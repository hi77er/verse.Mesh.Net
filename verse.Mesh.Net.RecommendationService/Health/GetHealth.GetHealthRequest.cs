namespace verse.Mesh.Net.RecommendationService.Health;

public class GetHealthRequest
{
  public const string Route = "/health";
  public static string BuildRoute() => Route;

  public string ServiceName { get; set; } = "Recommendation Service";
}
