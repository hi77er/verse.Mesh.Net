namespace verse.Mesh.Net.Core.Shared;

/// <summary>
///  A base class for DDD Entities. Includes support for domain events dispatched
///  post-persistence. If you prefer GUID Ids, change it here. If you need to support
///  both GUID and int IDs, change to EntityBase<TId> and use TId as the type for Id.
/// </summary>
public abstract class EntityBase : HasDomainEventsBase
{
}
