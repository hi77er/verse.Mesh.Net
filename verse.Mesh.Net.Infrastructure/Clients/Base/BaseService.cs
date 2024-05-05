namespace verse.Mesh.Net.Infrastructure.Clients.Base;
public class BaseService(WebApiClient webApiClient)
{
  protected readonly WebApiClient _webApiClient = webApiClient;
}
