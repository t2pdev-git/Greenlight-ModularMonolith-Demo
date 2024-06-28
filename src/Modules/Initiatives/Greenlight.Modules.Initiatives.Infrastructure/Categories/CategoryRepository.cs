using Greenlight.Modules.Initiatives.Domain.Categories;
using Greenlight.Modules.Initiatives.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Greenlight.Modules.Initiatives.Infrastructure.Categories;

internal sealed class CategoryRepository(InitiativesDbContext context) : ICategoryRepository
{
    public async Task<Category?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Categories.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Category category)
    {
        context.Categories.Add(category);
    }
}
