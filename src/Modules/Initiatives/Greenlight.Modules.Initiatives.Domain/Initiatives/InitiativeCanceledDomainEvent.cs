using Greenlight.Common.Domain;

namespace Greenlight.Modules.Initiatives.Domain.Initiatives;

public sealed class InitiativeCanceledDomainEvent(Guid initiativeId) : DomainEvent
{
    public Guid InitiativeId { get; init; } = initiativeId;
}
