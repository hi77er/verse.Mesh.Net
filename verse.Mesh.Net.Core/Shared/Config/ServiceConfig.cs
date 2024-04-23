using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace verse.Mesh.Net.Core.Shared.Config;

public class ServiceConfig
{
  public string Path { get; set; } = "/listallservices";

  public List<ServiceDescriptor> Services { get; set; } = new List<ServiceDescriptor>();

}
