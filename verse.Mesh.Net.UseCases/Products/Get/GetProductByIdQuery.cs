using Ardalis.Result;
using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.UseCases.Products.Get;

public record GetProductByIdQuery(Guid userId) : IQuery<Result<ProductDTO>>;
