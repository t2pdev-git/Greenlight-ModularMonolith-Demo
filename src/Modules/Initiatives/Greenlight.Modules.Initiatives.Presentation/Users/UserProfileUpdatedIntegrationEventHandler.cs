using Greenlight.Common.Application.EventBus;
using Greenlight.Common.Application.Exceptions;
using Greenlight.Common.Domain;
using Greenlight.Modules.Initiatives.Application.Users.UpdateUser;
using Greenlight.Modules.Users.IntegrationEvents;
using MediatR;

namespace Greenlight.Modules.Initiatives.Presentation.Users;

internal sealed class UserProfileUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserProfileUpdatedIntegrationEvent>
{
    public override async Task Handle(
        UserProfileUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateUserCommand(
                integrationEvent.UserId,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new GreenlightException(nameof(UpdateUserCommand), result.Error);
        }
    }
}
