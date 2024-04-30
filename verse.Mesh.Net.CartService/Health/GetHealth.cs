namespace verse.Mesh.Net.CartService.Health;

/// <summary>
/// An epoint representing a health status check.
/// </summary>
public static class GetHealth
{
  public static WebApplication MapHealthEndpoint(this WebApplication app)
  {
    app.MapGet("/health", async () =>
    {
      return await Task.FromResult("Cart Service is Alive!");
    });

    return app;
  }
}
