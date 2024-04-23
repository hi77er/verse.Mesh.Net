using System;
using Ardalis.GuardClauses;
using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.Core.CartAggregate;

public class Product(string name, decimal price, string description = "") : EntityBase
{
  public string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public string Description { get; set; } = description;
  public decimal Price { get; set; } = Guard.Against.Default(price, nameof(price));
  public Guid ImageMetadataId { get; set; }

}
