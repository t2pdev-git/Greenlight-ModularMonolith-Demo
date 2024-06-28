using System.Net;
using Greenlight.Common.Domain;
using Greenlight.Modules.Users.Application.Abstractions.Identity;
using Microsoft.Extensions.Logging;

namespace Greenlight.Modules.Users.Infrastructure.Identity;

internal sealed class IdentityProviderService(KeyCloakClient keyCloakClient, ILogger<IdentityProviderService> logger)
    : IIdentityProviderService
{
    private const string PasswordCredentialType = "Password";

    // POST /admin/realms/{realm}/users
    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]);

        try
        {
            string identityId = await keyCloakClient.RegisterUserAsync(userRepresentation, cancellationToken);

            return identityId;
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(exception, "User registration failed as EmailIsNotUnique");

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Unauthorized)
        {
            logger.LogError(exception, "User registration failed: Unauthorized");

            return Result.Failure<string>(IdentityProviderErrors.Unauthorized);
        }
    }
}
