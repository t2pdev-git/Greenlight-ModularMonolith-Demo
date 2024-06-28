using Greenlight.Common.Application.EventBus;
using Greenlight.Common.Application.Exceptions;
using Greenlight.Common.Domain;
using Greenlight.Modules.Initiatives.Application.Users.CreateUser;
using Greenlight.Modules.Users.IntegrationEvents;
using MediatR;

namespace Greenlight.Modules.Initiatives.Presentation.Users;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateUserCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new GreenlightException(nameof(CreateUserCommand), result.Error);
        }
    }
}
