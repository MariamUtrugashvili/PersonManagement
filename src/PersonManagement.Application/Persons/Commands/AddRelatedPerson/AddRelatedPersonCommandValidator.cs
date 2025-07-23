using FluentValidation;
using PersonManagement.Application.Constants;

namespace PersonManagement.Application.Persons.Commands.AddRelatedPerson
{
    public class AddRelatedPersonCommandValidator : AbstractValidator<AddRelatedPersonCommand>
    {
        public AddRelatedPersonCommandValidator()
        {
            RuleFor(x => x.PersonId)
                .GreaterThan(0).WithMessage(ValidationConstants.InvalidPersonId);

            RuleFor(x => x.RelatedToPersonId)
                .GreaterThan(0).WithMessage(ValidationConstants.InvalidPersonId)
                .NotEqual(x => x.PersonId).WithMessage("A person cannot be related to themselves");

            RuleFor(x => x.RelationType)
                .IsInEnum().WithMessage("Invalid relation type");
        }
    }
}
