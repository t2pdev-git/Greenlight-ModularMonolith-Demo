using Greenlight.Modules.Initiatives.Application.Abstractions.Data;
using Greenlight.Modules.Initiatives.Domain.Initiatives;
using MediatR;

namespace Greenlight.Modules.Initiatives.Application.Initiatives.CreateInitiative;

internal sealed class CreateInitiativeCommandHandler(
    IInitiativeRepository initiativeRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateInitiativeCommand, Guid>
{
    public async Task<Guid> Handle(CreateInitiativeCommand request, CancellationToken cancellationToken)
    {
        var initiative = Initiative.Create(request.Title);

        initiativeRepository.Insert(initiative);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return initiative.Id;
    }
}
