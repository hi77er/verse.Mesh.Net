using FastEndpoints;

namespace verse.Mesh.Net.ShippingService.Health;

/// <summary>
/// An epoint representing a health status check.
/// </summary>
public class GetHealth : Endpoint<GetHealthRequest, string>
{
  public override void Configure()
  {
    Get(GetHealthRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    GetHealthRequest request,
    CancellationToken cancellationToken)
  {
    Response = $"{request.ServiceName} is Alive!";

    await Task.CompletedTask;

    return;
  }
}

