using Greenlight.Common.Application.Messaging;

namespace Greenlight.Modules.Initiatives.Application.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid CategoryId, string Name) : ICommand;
