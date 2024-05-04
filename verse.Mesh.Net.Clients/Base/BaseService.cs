namespace verse.Mesh.Net.Clients.Base;
public class BaseService(WebApiClient webApiClient)
{
  protected readonly WebApiClient _webApiClient = webApiClient;
}
