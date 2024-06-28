using Greenlight.Common.Domain;
using Greenlight.IntegrationTests.Abstractions;
using Greenlight.Modules.Initiatives.Application.Users.GetUser;
using Greenlight.Modules.Users.Application.Users.RegisterUser;
using Shouldly;

namespace Greenlight.IntegrationTests.RegisterUser;
public sealed class RegisterUserTests : BaseIntegrationTest
{
    public RegisterUserTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task RegisterUser_Should_PropagateToInitiativeModule()
    {
        // Register user
        var command = new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Internet.Password(8),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        Result<Guid> userQueryResult = await Sender.Send(command);

        userQueryResult.Error.Description.ShouldBeEmpty();

        // Get Initiative User
        Result<UserResponse> initiativeUserResult = await Poller.WaitAsync(
            TimeSpan.FromSeconds(15),
            async () =>
            {
                var query = new GetUserQuery(userQueryResult.Value);

                Result<UserResponse> userResult = await Sender.Send(query);

                return userResult;
            });

        // Assert
        initiativeUserResult.Error.Description.ShouldBeEmpty();
        initiativeUserResult.Value.ShouldNotBeNull();
    }
}
