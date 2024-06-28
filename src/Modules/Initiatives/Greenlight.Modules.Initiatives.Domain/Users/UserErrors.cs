using Greenlight.Common.Domain;

namespace Greenlight.Modules.Initiatives.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) =>
        Error.NotFound("User.NotFound", $"The user with the user id {userId} was not found");
}
