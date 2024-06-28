namespace Greenlight.Modules.Initiatives.Application.Initiatives.GetInitiative;

public sealed record InitiativeResponse(
    Guid Id,
    string Title,
    string Description,
    Guid CategoryId);
