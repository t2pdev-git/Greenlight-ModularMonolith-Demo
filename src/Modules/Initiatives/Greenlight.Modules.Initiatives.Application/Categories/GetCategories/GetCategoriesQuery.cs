using Greenlight.Common.Application.Messaging;
using Greenlight.Modules.Initiatives.Application.Categories.GetCategory;

namespace Greenlight.Modules.Initiatives.Application.Categories.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IReadOnlyCollection<CategoryResponse>>;

