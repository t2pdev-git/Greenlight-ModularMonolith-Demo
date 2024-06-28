using Greenlight.Common.Application.Messaging;

namespace Greenlight.Modules.Initiatives.Application.Users.CreateUser;

public sealed record CreateUserCommand(Guid UserId, string Email, string FirstName, string LastName)
    : ICommand;
