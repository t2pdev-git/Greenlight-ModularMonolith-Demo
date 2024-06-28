using Greenlight.Modules.Initiatives.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greenlight.Modules.Initiatives.Infrastructure.Categories;

internal sealed class CategoryDbConfig : IEntityTypeConfiguration<Category>
{
    internal const string HealthAndSafetyName = "Health and Safety";
    internal const string EmployeeEngagementName = "Employee Engagement";
    internal const string EmployeeLearningAndGrowthName = "Employee Learning and Growth";
    internal const string CustomerServiceName = "Customer Service";
    internal const string InternalProcessesName = "Internal Processes";

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(GetDefaultCategories());

        builder.Property(c => c.Name)
            .HasMaxLength(Category.NameMaxLength);
    }

    private IEnumerable<Category> GetDefaultCategories()
    {
        yield return Category.CategoryNotSet;
        yield return Category.Create(HealthAndSafetyName);
        yield return Category.Create(EmployeeEngagementName);
        yield return Category.Create(EmployeeLearningAndGrowthName);
        yield return Category.Create(CustomerServiceName);
        yield return Category.Create(InternalProcessesName);
    }
}
