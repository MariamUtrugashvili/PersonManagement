using FluentValidation;
using PersonManagement.Application.Constants;

namespace PersonManagement.Application.Persons.Common
{
    public class PersonCommandValidatorBase<T> : AbstractValidator<T>
        where T : PersonCommandBase
    {
        public PersonCommandValidatorBase()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(ValidationConstants.FirstNameRequired)
                .Length(2, 50).WithMessage(ValidationConstants.FirstNameLength)
                .Must(PersonValidationExtensions.BeOnlyLatinOrGeorgian)
                .WithMessage(ValidationConstants.FirstNameLatinOrGeorgian);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(ValidationConstants.LastNameRequired)
                .Length(2, 50).WithMessage(ValidationConstants.LastNameLength)
                .Must(PersonValidationExtensions.BeOnlyLatinOrGeorgian)
                .WithMessage(ValidationConstants.LastNameLatinOrGeorgian);

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage(ValidationConstants.GenderInvalid);

            RuleFor(x => x.PersonalNumber)
                .NotEmpty().WithMessage(ValidationConstants.PersonalNumberRequired)
                .Matches(@"^\d{11}$").WithMessage(ValidationConstants.PersonalNumberFormat);

            RuleFor(x => x.DateOfBirth)
                .Must(PersonValidationExtensions.BeAtLeast18YearsOld)
                .WithMessage(ValidationConstants.DateOfBirth18);

            RuleForEach(x => x.PhoneNumbers).ChildRules(phone =>
            {
                phone.RuleFor(p => p.PhoneNumberType).IsInEnum().WithMessage(ValidationConstants.PhoneNumberTypeInvalid);
                phone.RuleFor(p => p.Number).Length(4, 50).WithMessage(ValidationConstants.PhoneNumberLength);
            });
        }
    }
}
