using FluentValidation;
using PersonManagement.Application.Constants;
using PersonManagement.Application.Persons.Common;

namespace PersonManagement.Application.Persons.Queries.SearchPersons
{
    public class SearchPersonsQueryValidator : AbstractValidator<SearchPersonsQuery>
    {
        public SearchPersonsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage(ValidationConstants.PageNumberGreaterThanZero);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage(ValidationConstants.PageSizeRange);

            RuleFor(x => x.FirstName)
            .Length(2, 50)
            .WithMessage(ValidationConstants.FirstNameLength)
            .Must(PersonValidationExtensions.BeOnlyLatinOrGeorgian)
            .WithMessage(ValidationConstants.FirstNameLatinOrGeorgian)
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

            RuleFor(x => x.LastName)
                .Length(2, 50)
                .WithMessage(ValidationConstants.LastNameLength)
                .Must(PersonValidationExtensions.BeOnlyLatinOrGeorgian)
                .WithMessage(ValidationConstants.LastNameLatinOrGeorgian)
                .When(x => !string.IsNullOrWhiteSpace(x.LastName));

            RuleFor(x => x.PersonalNumber)
                .Matches(@"^\d{11}$")
                .WithMessage(ValidationConstants.PersonalNumberFormat)
                .When(x => !string.IsNullOrWhiteSpace(x.PersonalNumber));
        }
    }
}
