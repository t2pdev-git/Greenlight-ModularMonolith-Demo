using Greenlight.Common.Application.Messaging;
using Greenlight.Common.Domain;
using Greenlight.Modules.Initiatives.Application.Abstractions.Data;
using Greenlight.Modules.Initiatives.Domain.Categories;

namespace Greenlight.Modules.Initiatives.Application.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name);

        categoryRepository.Insert(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
