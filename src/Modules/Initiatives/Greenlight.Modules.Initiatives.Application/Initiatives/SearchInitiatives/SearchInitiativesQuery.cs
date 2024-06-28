using Greenlight.Common.Application.Messaging;

namespace Greenlight.Modules.Initiatives.Application.Initiatives.SearchInitiatives;

public sealed record SearchInitiativesQuery(
    Guid? CategoryId,
    string? Title,
    string? Description,
    int Page,
    int PageSize) : IQuery<SearchInitiativesResponse>;
