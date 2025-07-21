using FluentValidation;

namespace PersonManagement.Application.Persons.Commands.DeletePerson
{
    public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Person Id must be a positive integer.");
        }
    }
}
