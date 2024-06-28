using Greenlight.Modules.Initiatives.Application.Initiatives.GetInitiative;

namespace Greenlight.Modules.Initiatives.Application.Initiatives.SearchInitiatives;

public sealed record SearchInitiativesResponse(
    int Page,
    int PageSize, 
    int TotalCount,
    IReadOnlyCollection<InitiativeResponse> Initiatives);

