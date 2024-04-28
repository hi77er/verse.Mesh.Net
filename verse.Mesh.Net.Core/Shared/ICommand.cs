using MediatR;

namespace verse.Mesh.Net.Core.Shared;

/// <summary>
/// Summary:
///     Source: https://code-maze.com/cqrs-mediatr-fluentvalidation/
///
/// Type parameters:
///   TResponse:
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>, IBaseRequest
{
}
