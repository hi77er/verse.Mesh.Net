using Ardalis.GuardClauses;
using verse.Mesh.Net.Core.Shared;

namespace verse.Mesh.Net.Core.CartAggregate.Events;

/// <summary>
/// A domain event that is dispatched whenever an order gets cancelled.
/// The OrderService is used to dispatch this event.
/// </summary>
internal class OrderCancelledEvent(Guid orderId) : DomainEventBase
{
  public Guid OrderId { get; set; } = Guard.Against.NullOrEmpty(orderId);
}
