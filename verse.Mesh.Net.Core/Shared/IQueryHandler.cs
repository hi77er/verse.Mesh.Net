using MediatR;

namespace verse.Mesh.Net.Core.Shared;

/// <summary>
/// Summary:
///     Source: https://code-maze.com/cqrs-mediatr-fluentvalidation/
///
/// Type parameters:
///   TQuery:
///
///   TResponse:
/// </summary>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
}
