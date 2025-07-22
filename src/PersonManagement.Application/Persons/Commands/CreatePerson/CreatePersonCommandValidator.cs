using FluentValidation;
using PersonManagement.Application.Persons.Common;
using System.Text.RegularExpressions;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : PersonCommandValidatorBase<CreatePersonCommand>
    {
        public CreatePersonCommandValidator() : base()
        {
        }
    }

}
