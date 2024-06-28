using Greenlight.Modules.Initiatives.Domain.Users;
using Greenlight.Modules.Initiatives.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Greenlight.Modules.Initiatives.Infrastructure.Users;


internal sealed class UserRepository(InitiativesDbContext context) : IUserRepository
{
    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}
