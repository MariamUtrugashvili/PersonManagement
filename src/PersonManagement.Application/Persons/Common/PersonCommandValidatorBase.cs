using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonManagement.Application.Persons.Common
{
    public class PersonCommandValidatorBase<T> : AbstractValidator<T>
        where T : PersonCommandBase
    {
        public PersonCommandValidatorBase()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(2, 50)
                .Must(BeOnlyLatinOrGeorgian)
                .WithMessage("First name must contain only Latin or Georgian letters.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(2, 50)
                .Must(BeOnlyLatinOrGeorgian)
                .WithMessage("Last name must contain only Latin or Georgian letters.");

            RuleFor(x => x.Gender)
                .IsInEnum();

            RuleFor(x => x.PersonalNumber)
                .NotEmpty()
                .Matches(@"^\d{11}$");

            RuleFor(x => x.DateOfBirth)
                .Must(BeAtLeast18YearsOld)
                .WithMessage("Person must be at least 18 years old.");

            RuleForEach(x => x.PhoneNumbers).ChildRules(phone =>
            {
                phone.RuleFor(p => p.PhoneNumberType).IsInEnum();
                phone.RuleFor(p => p.Number).Length(4, 50);
            });
        }

        private bool BeOnlyLatinOrGeorgian(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            var isLatin = Regex.IsMatch(name, @"^[a-zA-Z]+$");
            var isGeorgian = Regex.IsMatch(name, @"^[ა-ჰ]+$");
            return isLatin ^ isGeorgian;
        }

        private bool BeAtLeast18YearsOld(DateTime dob)
        {
            return dob <= DateTime.Today.AddYears(-18);
        }
    }

}
