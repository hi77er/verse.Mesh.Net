using System.Diagnostics;
using System.Reflection;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace verse.Mesh.Net.Core.Shared.Behavior;

/// <summary>
///  Adds logging for all requests in MediatR pipeline. Configure by adding the service
///  with a scoped lifetime Example for Autofac: builder .RegisterType<Mediator>()
///  .As<IMediator>() .InstancePerLifetimeScope(); builder .RegisterGeneric(typeof(LoggingBehavior<,>))
///  .As(typeof(IPipelineBehavior<,>)) .InstancePerLifetimeScope();
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
  private readonly ILogger<Mediator> _logger;

  public LoggingBehavior(ILogger<Mediator> logger)
  {
    _logger = logger;
  }

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    Guard.Against.Null(request, "request");
    if (_logger.IsEnabled(LogLevel.Information))
    {
      _logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);
      foreach (PropertyInfo item in (IEnumerable<PropertyInfo>)new List<PropertyInfo>(request.GetType().GetProperties()))
      {
        var obj = item?.GetValue(request, null);
        _logger.LogInformation("Property {Property} : {@Value}", item?.Name, obj);
      }
    }

    Stopwatch sw = Stopwatch.StartNew();
    TResponse val = await next();
    _logger.LogInformation("Handled {RequestName} with {Response} in {ms} ms", typeof(TRequest).Name, val, sw.ElapsedMilliseconds);
    sw.Stop();
    return val;
  }
}
