using MediatR;

namespace Greenlight.Modules.Initiatives.Application.Initiatives.GetInitiative;

public sealed record GetInitiativeQuery(Guid InitiativeId) 
    : IRequest<InitiativeResponse?>;
