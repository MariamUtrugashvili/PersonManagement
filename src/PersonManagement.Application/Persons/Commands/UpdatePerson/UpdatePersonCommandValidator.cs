using FluentValidation;
using PersonManagement.Application.Persons.Commands.CreatePerson;
using PersonManagement.Application.Persons.Common;

namespace PersonManagement.Application.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator : PersonCommandValidatorBase<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator() : base()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid person ID.");
        }
    }
}
