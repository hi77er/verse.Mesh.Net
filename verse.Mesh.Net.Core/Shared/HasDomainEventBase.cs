using System.ComponentModel.DataAnnotations.Schema;

namespace verse.Mesh.Net.Core.Shared;

public abstract class HasDomainEventsBase
{
  private List<DomainEventBase> _domainEvents = new List<DomainEventBase>();

  [NotMapped]
  public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

  protected void RegisterDomainEvent(DomainEventBase domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }

  internal void ClearDomainEvents()
  {
    _domainEvents.Clear();
  }
}
