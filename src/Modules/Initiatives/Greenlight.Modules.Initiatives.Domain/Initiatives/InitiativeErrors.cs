using Greenlight.Common.Domain;

namespace Greenlight.Modules.Initiatives.Domain.Initiatives;

public static class InitiativeErrors
{
    private const string ErrorGroupName = "Initiatives";

    public static readonly Error AlreadyCompleted = Error.Problem(
        $"{ErrorGroupName}.AlreadyCompleted",
        "The Initiative has already been marked as Completed");

    public static readonly Error AlreadyCanceled = Error.Problem(
        $"{ErrorGroupName}.AlreadyCanceled",
        "The Initiative has already been canceled");

    public static readonly Error CancellationReasonIsRequired = Error.Problem(
        $"{ErrorGroupName}.CancellationReasonIsRequired",
        "A Cancellation Reason is required");

    public static readonly Error CannotCancelCompleted = Error.Problem(
        $"{ErrorGroupName}.CannotCancelCompleted",
        "A Completed Initiative cannot be canceled");

    public static readonly Error NotDraft = Error.Problem(
        $"{ErrorGroupName}.NotDraft",
        "The Initiative is not in Draft status");

    public static Error NotFound(Guid initiativeId) =>
        Error.NotFound(
            $"{ErrorGroupName}.NotFound",
            $"The Initiative with identifier {initiativeId} was not found");
}
