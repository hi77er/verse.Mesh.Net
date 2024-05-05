using System.Text;
using System.Text.Json;

namespace verse.Mesh.Net.Infrastructure.Clients.Base;

public class WebApiClient
{
  private readonly HttpClient _httpClient;

  public WebApiClient(string baseUrl)
  {
    _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
  }

  public async Task<TResponse?> GetAsync<TResponse>(string url)
  {
    var response = await _httpClient.GetAsync(url);
    response.EnsureSuccessStatusCode(); // Throw exception for non-2xx status codes

    var contentString = await response.Content.ReadAsStringAsync();

    if (contentString is null) 
      return default;

    return JsonSerializer.Deserialize<TResponse>(contentString);
  }

  public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest requestData)
  {
    var contentString = JsonSerializer.Serialize(requestData);
    var content = new StringContent(contentString, Encoding.UTF8, "application/json");

    var response = await _httpClient.PostAsync(url, content);
    response.EnsureSuccessStatusCode();

    var responseString = await response.Content.ReadAsStringAsync();

    if (contentString is null)
      return default;

    return JsonSerializer.Deserialize<TResponse>(responseString);
  }
}
