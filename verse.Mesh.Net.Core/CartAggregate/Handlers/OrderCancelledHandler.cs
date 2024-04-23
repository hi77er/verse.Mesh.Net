using MediatR;
using Microsoft.Extensions.Logging;
using verse.Mesh.Net.Core.CartAggregate.Events;

namespace verse.Mesh.Net.Core.CartAggregate.Handlers;

internal class OrderCancelledHandler(ILogger<OrderCancelledHandler> logger) 
  : INotificationHandler<OrderCancelledEvent>
{
  private readonly ILogger _logger = logger;

  public async Task Handle(OrderCancelledEvent domainEvent, CancellationToken cancellationToken)
  {
    logger.LogInformation("Handling Order Canceled event for {contributorId}", domainEvent.OrderId);

    //await emailSender.SendEmailAsync("to@test.com",
    //                                 "from@test.com",
    //                                 "Order Canceled",
    //                                 $"Contributor with id {domainEvent.ContributorId} was deleted.");
    await Task.CompletedTask;
  }
}
