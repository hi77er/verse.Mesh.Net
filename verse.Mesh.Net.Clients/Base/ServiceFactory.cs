﻿using Ardalis.GuardClauses;
using verse.Mesh.Net.Clients.Facade;
using verse.Mesh.Net.Clients.Services;

namespace verse.Mesh.Net.Clients.Base;
public static class ServiceFactory
{
  public static IInventoryService CreateInventoryService(string baseUrl)
  {
    Guard.Against.NullOrEmpty(baseUrl);

    var webApiClient = new WebApiClient(baseUrl);
    var inventoryService = new InventoryService(webApiClient);
    
    return inventoryService;
  }
}
