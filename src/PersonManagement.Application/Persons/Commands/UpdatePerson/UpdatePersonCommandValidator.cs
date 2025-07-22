using FluentValidation;
using PersonManagement.Application.Persons.Commands.CreatePerson;

namespace PersonManagement.Application.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid person ID.");
        }
    }
}
