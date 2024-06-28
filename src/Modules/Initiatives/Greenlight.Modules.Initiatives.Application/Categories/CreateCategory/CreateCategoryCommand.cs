using Greenlight.Common.Application.Messaging;

namespace Greenlight.Modules.Initiatives.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Guid>;
