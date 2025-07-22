using FluentValidation;
using PersonManagement.Application.Persons.Common;
using System.Text.RegularExpressions;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : PersonCommandValidatorBase<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
           
            RuleForEach(x => x.RelatedPersons).ChildRules(rp =>
            {
                rp.RuleFor(r => r.RelatedToPersonId)
                    .GreaterThan(0).WithMessage("Related person ID must be greater than 0");

                rp.RuleFor(r => r.RelationType)
                    .IsInEnum().WithMessage("Relation type is invalid");
            });
        }
    }

}
