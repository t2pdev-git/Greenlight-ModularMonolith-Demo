using Greenlight.Common.Application.Authorization;
using Greenlight.Common.Domain;
using Greenlight.Modules.Users.Application.Users.GetUserPermissions;
using Greenlight.Modules.Users.Application.Users.RegisterUser;
using Greenlight.Modules.Users.Domain.Users;
using Greenlight.Modules.Users.IntegrationTests.Abstractions;
using Shouldly;

namespace Greenlight.Modules.Users.IntegrationTests.Users;

public class GetUserPermissionTests : BaseIntegrationTest
{
    public GetUserPermissionTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        string identityId = Guid.NewGuid().ToString();

        // Act
        Result<PermissionsResponse> permissionsResult = 
            await Sender.Send(new GetUserPermissionsQuery(identityId));

        // Assert
        permissionsResult.Error.ShouldBe(UserErrors.NotFound(identityId));
    }

    [Fact]
    public async Task Should_ReturnPermissions_WhenUserExists()
    {
        // Arrange
        Result<Guid> result = await Sender.Send(new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Internet.Password(),
            Faker.Name.FirstName(),
            Faker.Name.LastName()));

        string identityId = DbContext.Users.Single(u => u.Id == result.Value).IdentityId;

        // Act
        Result<PermissionsResponse> permissionsResult = await Sender.Send(new GetUserPermissionsQuery(identityId));

        // Assert
        permissionsResult.IsSuccess.ShouldBeTrue();
        permissionsResult.Value.Permissions.ShouldNotBeEmpty();
    }
}
