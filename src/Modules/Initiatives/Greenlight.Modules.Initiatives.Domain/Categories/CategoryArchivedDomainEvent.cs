using Greenlight.Common.Domain;

namespace Greenlight.Modules.Initiatives.Domain.Categories;
public sealed class CategoryArchivedDomainEvent(Guid categoryId) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;
}
