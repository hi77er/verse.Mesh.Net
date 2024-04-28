using Ardalis.GuardClauses;
using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.Core.ProductAggregate;

public class Product(string name, decimal price, string? description = null) : EntityBase
{
  public string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public string? Description { get; set; } = description;
  public decimal Price { get; set; } = Guard.Against.Default(price, nameof(price));
  public Guid ImageMetadataId { get; set; }

}
