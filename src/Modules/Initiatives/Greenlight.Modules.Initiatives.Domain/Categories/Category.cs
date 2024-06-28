using Greenlight.Common.Domain;

namespace Greenlight.Modules.Initiatives.Domain.Categories;
public sealed class Category : Entity
{
    public const int NameMaxLength = 40;

    public static readonly Category CategoryNotSet = new ()
    {
        Id = Guid.Parse("CA000000-0000-0000-0000-000000000000"),
        Name = "Category Not Set",
        IsArchived = false,
        IsEditable = false
    };

    private Category()
    {
    }

    public override Guid Id { get; protected set; }

    public string Name { get; private set; }

    public bool IsArchived { get; private set; }

    public bool IsEditable { get; init; }

    public static Category Create(string name)
    {
        return Create(Guid.NewGuid(), name, false, true);
    }

    private static Category Create(Guid id, string name, bool isArchived, bool isEditable)
    {
        Category category = new()
        {
            Id = id,
            Name = name,
            IsArchived = isArchived,
            IsEditable = isEditable
        };

        category.Raise(new CategoryCreatedDomainEvent(category.Id));

        return category;
    }

    public Result Archive()
    {
        if (!IsEditable)
        {
            return Result.Failure(CategoryErrors.NotEditable);
        }

        IsArchived = true;

        Raise(new CategoryArchivedDomainEvent(Id));

        return Result.Success();
    }

    public Result ChangeName(string name)
    {
        if (Name == name)
        {
            return Result.Success();
        }

        if (!IsEditable)
        {
            return Result.Failure(CategoryErrors.NotEditable);
        }

        Name = name;

        Raise(new CategoryNameChangedDomainEvent(Id, Name));

        return Result.Success();
    }
}
