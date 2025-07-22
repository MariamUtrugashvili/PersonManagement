using FluentValidation;

namespace PersonManagement.Application.Persons.Commands.DeleteRelatedPerson
{
    public class DeleteRelatedPersonCommandValidator : AbstractValidator<DeleteRelatedPersonCommand>
    {
        public DeleteRelatedPersonCommandValidator()
        {
            RuleFor(x => x.PersonId)
                .GreaterThan(0).WithMessage("Invalid person ID.");

            RuleFor(x => x.RelatedToPersonId)
                .GreaterThan(0)
                .NotEqual(x => x.PersonId).WithMessage("A person cannot be related to themselves");

        }
    }
}
