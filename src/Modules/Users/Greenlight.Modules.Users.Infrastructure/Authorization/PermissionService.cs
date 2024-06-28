using Greenlight.Common.Application.Authorization;
using Greenlight.Common.Domain;
using Greenlight.Modules.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace Greenlight.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
