using FluentValidation;
using System.Text.RegularExpressions;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters")
                .Must(BeOnlyLatinOrGeorgian).WithMessage("First name must contain only Georgian or only Latin letters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters")
                .Must(BeOnlyLatinOrGeorgian).WithMessage("Last name must contain only Georgian or only Latin letters");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender is invalid");

            RuleFor(x => x.PersonalNumber)
                .NotEmpty().WithMessage("Personal number is required")
                .Matches(@"^\d{11}$").WithMessage("Personal number must be exactly 11 digits");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .Must(BeAtLeast18YearsOld).WithMessage("Person must be at least 18 years old");

            RuleForEach(x => x.PhoneNumbers).ChildRules(phone =>
            {
                phone.RuleFor(p => p.PhoneNumberType)
                    .IsInEnum().WithMessage("Phone number type is invalid");

                phone.RuleFor(p => p.Number)
                    .NotEmpty().WithMessage("Phone number is required")
                    .Length(4, 50).WithMessage("Phone number must be between 4 and 50 characters");
            });

            RuleForEach(x => x.RelatedPersons).ChildRules(rp =>
            {
                rp.RuleFor(r => r.RelatedToPersonId)
                    .GreaterThan(0).WithMessage("Related person ID must be greater than 0");

                rp.RuleFor(r => r.RelationType)
                    .IsInEnum().WithMessage("Relation type is invalid");
            });
        }

        private bool BeAtLeast18YearsOld(DateTime dob)
        {
            return dob <= DateTime.Today.AddYears(-18);
        }

        private bool BeOnlyLatinOrGeorgian(string name)
        {
            var isLatin = Regex.IsMatch(name, @"^[a-zA-Z]+$");
            var isGeorgian = Regex.IsMatch(name, @"^[ა-ჰ]+$");
            return isLatin ^ isGeorgian;
        }
    }

}
