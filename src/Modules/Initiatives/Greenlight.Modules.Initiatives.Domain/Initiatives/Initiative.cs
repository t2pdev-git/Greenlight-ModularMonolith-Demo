using Greenlight.Common.Domain;
using Greenlight.Modules.Initiatives.Domain.Categories;

namespace Greenlight.Modules.Initiatives.Domain.Initiatives;

public sealed class Initiative : Entity
{
    public const int TitleMaxLength = 100;

    // :DLO:0 Add the property Initiative.InitiativeNumber   

    public override Guid Id { get; protected set; }

    // :DLO:0 Add Title class - primitive obsession
    public string Title { get; private set; }

    // :DLO:0 Replace property Initiative.Description with PurposeStatement
    // Kelley, D. Lynn; Shook, John. Change Questions: A Playbook for Effective and Lasting Organizational Change (p. 42). First Modus Cooperandi Press. Kindle Edition.    
    public string Description { get; private set; }

    // :DLO:0 Add property Initiative.ValueStatement
    // Kelley, D. Lynn; Shook, John. Change Questions: A Playbook for Effective and Lasting Organizational Change (p. 42). First Modus Cooperandi Press. Kindle Edition.    

    public Guid CategoryId { get; private set; }

    public InitiativeProcessStatus ProcessStatus { get; private set; }

    // :DLO:0 Add property Initiative.CancellationReason (nullable string)

    private Initiative()
    {
    }

    public static Initiative Create(string title)
    {
        Initiative instance = new()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = title,
            ProcessStatus = InitiativeProcessStatus.Draft,
            CategoryId = Category.CategoryNotSet.Id
        };

        instance.Raise(new InitiativeCreatedDomainEvent(instance.Id));

        return instance;
    }

    public Result SubmitForApproval()
    {
        if (ProcessStatus != InitiativeProcessStatus.Draft)
        {
            return Result.Failure(InitiativeErrors.NotDraft);
        }

        ProcessStatus = InitiativeProcessStatus.SubmittedForApproval;

        Raise(new InitiativeSubmittedForApprovalDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel(string cancellationReason)
    {
        if (ProcessStatus == InitiativeProcessStatus.Canceled)
        {
            return Result.Failure(InitiativeErrors.AlreadyCanceled);
        }

        if (ProcessStatus == InitiativeProcessStatus.Completed)
        {
            return Result.Failure(InitiativeErrors.CannotCancelCompleted);
        }

        if (string.IsNullOrWhiteSpace(cancellationReason))
        {
            return Result.Failure(InitiativeErrors.CancellationReasonIsRequired);
        }

        ProcessStatus = InitiativeProcessStatus.Canceled;
        // :DLO:0 Set the property Initiative.CancellationReason value

        Raise(new InitiativeCanceledDomainEvent(Id));

        return Result.Success();
    }

}
