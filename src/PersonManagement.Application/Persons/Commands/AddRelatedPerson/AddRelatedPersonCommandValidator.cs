using FluentValidation;

namespace PersonManagement.Application.Persons.Commands.AddRelatedPerson
{
    public class AddRelatedPersonCommandValidator : AbstractValidator<AddRelatedPersonCommand>
    {
        public AddRelatedPersonCommandValidator()
        {
            RuleFor(x => x.PersonId)
            .GreaterThan(0);

            RuleFor(x => x.RelatedToPersonId)
                .GreaterThan(0)
                .NotEqual(x => x.PersonId).WithMessage("A person cannot be related to themselves");

            RuleFor(x => x.RelationType)
                .IsInEnum().WithMessage("Invalid relation type");
        }
    }
}
