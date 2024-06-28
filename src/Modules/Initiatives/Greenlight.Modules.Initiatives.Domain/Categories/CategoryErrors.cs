using Greenlight.Common.Domain;

namespace Greenlight.Modules.Initiatives.Domain.Categories;
public static class CategoryErrors
{
    private const string ErrorGroupName = "Categories";

    public static readonly Error AlreadyArchived = Error.Problem(
        $"{ErrorGroupName}.AlreadyArchived",
        "The Category was already archived");

    public static readonly Error NotEditable = Error.Problem(
        $"{ErrorGroupName}.NotEditable",
        "The Category is cannot be edited or archived as it is used as a default value");

    public static Error NotFound(Guid categoryId) =>
        Error.NotFound(
            $"{ErrorGroupName}.NotFound", 
            $"The Category with identifier {categoryId} was not found");
}
