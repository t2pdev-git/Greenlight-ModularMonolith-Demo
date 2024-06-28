using Greenlight.Common.Application.Authorization;
using Greenlight.Common.Application.Messaging;

namespace Greenlight.Modules.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
