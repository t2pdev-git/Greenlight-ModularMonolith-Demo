using Greenlight.Common.Application.Messaging;

namespace Greenlight.Modules.Initiatives.Application.Categories.GetCategory;

public sealed record GetCategoryQuery(Guid CategoryId) : IQuery<CategoryResponse>;
