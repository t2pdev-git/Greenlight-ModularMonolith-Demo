using Greenlight.Common.Domain;

namespace Greenlight.Modules.Initiatives.Domain.Categories;

public sealed class CategoryCreatedDomainEvent(Guid categoryId) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;
}
