using Greenlight.Common.Application.Messaging;

namespace Greenlight.Modules.Initiatives.Application.Categories.ArchiveCategory;

public sealed record ArchiveCategoryCommand(Guid CategoryId) : ICommand;
