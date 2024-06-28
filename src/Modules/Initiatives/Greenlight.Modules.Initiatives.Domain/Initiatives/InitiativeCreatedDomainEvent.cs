using Greenlight.Common.Domain;

namespace Greenlight.Modules.Initiatives.Domain.Initiatives;
public sealed class InitiativeCreatedDomainEvent(Guid eventId) : DomainEvent
{
    public Guid InitiativeId { get; init; } = eventId;
}
