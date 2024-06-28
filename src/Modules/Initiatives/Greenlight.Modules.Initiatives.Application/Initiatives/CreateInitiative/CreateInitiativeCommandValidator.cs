using FluentValidation;

namespace Greenlight.Modules.Initiatives.Application.Initiatives.CreateInitiative;

internal sealed class CreateInitiativeCommandValidator : AbstractValidator<CreateInitiativeCommand>
{
    // :DLO:0 For the validator CreateInitiativeCommandValidator: how to create a custom code for the error that includes the Property Name.
    //   - right now it is -> "code": "NotEmptyValidator", "description": "'Name' must not be empty.", "type": 2

    public CreateInitiativeCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty();
        RuleFor(c => c.Description)
            .NotEmpty();
    }
}
