using FluentValidation;

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
                .Must(PersonValidationExtensions.BeOnlyLatinOrGeorgian)
                .WithMessage("First name must contain only Latin or Georgian letters.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(2, 50)
                .Must(PersonValidationExtensions.BeOnlyLatinOrGeorgian)
                .WithMessage("Last name must contain only Latin or Georgian letters.");

            RuleFor(x => x.Gender)
                .IsInEnum();

            RuleFor(x => x.PersonalNumber)
                .NotEmpty()
                .Matches(@"^\d{11}$");

            RuleFor(x => x.DateOfBirth)
                .Must(PersonValidationExtensions.BeAtLeast18YearsOld)
                .WithMessage("Person must be at least 18 years old.");

            RuleForEach(x => x.PhoneNumbers).ChildRules(phone =>
            {
                phone.RuleFor(p => p.PhoneNumberType).IsInEnum();
                phone.RuleFor(p => p.Number).Length(4, 50);
            });
        }
    }
}
