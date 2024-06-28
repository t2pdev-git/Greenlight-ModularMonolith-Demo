using MediatR;

namespace Greenlight.Modules.Initiatives.Application.Initiatives.CreateInitiative;

public sealed record CreateInitiativeCommand(string Title, string Description) 
    : IRequest<Guid>;
