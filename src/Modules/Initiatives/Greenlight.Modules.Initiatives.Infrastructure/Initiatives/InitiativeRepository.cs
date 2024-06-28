using Greenlight.Modules.Initiatives.Domain.Initiatives;
using Greenlight.Modules.Initiatives.Infrastructure.Database;

namespace Greenlight.Modules.Initiatives.Infrastructure.Initiatives;
internal sealed class InitiativeRepository(InitiativesDbContext context) : IInitiativeRepository
{
    public void Insert(Initiative initiative)
    {
        context.Initiatives.Add(initiative);
    }
}
