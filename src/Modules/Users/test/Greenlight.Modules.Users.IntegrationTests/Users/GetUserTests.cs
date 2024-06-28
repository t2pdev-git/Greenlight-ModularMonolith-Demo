using Greenlight.Common.Domain;
using Greenlight.Modules.Users.Application.Users.GetUser;
using Greenlight.Modules.Users.Application.Users.RegisterUser;
using Greenlight.Modules.Users.Domain.Users;
using Greenlight.Modules.Users.IntegrationTests.Abstractions;
using Shouldly;

namespace Greenlight.Modules.Users.IntegrationTests.Users;

public class GetUserTests : BaseIntegrationTest
{
    public GetUserTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        Result<UserResponse> userResult = await Sender.Send(new GetUserQuery(userId));

        // Assert
        userResult.Error.ShouldBe(UserErrors.NotFound(userId));
    }

    [Fact]
    public async Task Should_ReturnUser_WhenUserExists()
    {
        // Arrange
        Result<Guid> result = await Sender.Send(new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Internet.Password(),
            Faker.Name.FirstName(),
            Faker.Name.LastName()));
        Guid userId = result.Value;

        // Act
        Result<UserResponse> userResult = await Sender.Send(new GetUserQuery(userId));

        // Assert
        userResult.IsSuccess.ShouldBeTrue();
        userResult.Value.ShouldNotBeNull();
    }
}
