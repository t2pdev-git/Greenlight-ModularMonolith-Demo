using Greenlight.Common.Application.Messaging;

namespace Greenlight.Modules.Initiatives.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
