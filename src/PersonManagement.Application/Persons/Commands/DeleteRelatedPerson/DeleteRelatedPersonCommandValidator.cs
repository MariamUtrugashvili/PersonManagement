using FluentValidation;
using PersonManagement.Application.Constants;

namespace PersonManagement.Application.Persons.Commands.DeleteRelatedPerson
{
    public class DeleteRelatedPersonCommandValidator : AbstractValidator<DeleteRelatedPersonCommand>
    {
        public DeleteRelatedPersonCommandValidator()
        {
            RuleFor(x => x.PersonId)
                .GreaterThan(0).WithMessage(ValidationConstants.InvalidPersonId);

            RuleFor(x => x.RelatedToPersonId)
                .GreaterThan(0).WithMessage(ValidationConstants.InvalidPersonId)
                .NotEqual(x => x.PersonId).WithMessage(ValidationConstants.CannotRelateToSelf);
        }
    }
}
