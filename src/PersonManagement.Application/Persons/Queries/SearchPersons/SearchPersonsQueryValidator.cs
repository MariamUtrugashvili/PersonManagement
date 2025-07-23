using FluentValidation;
using PersonManagement.Application.Persons.Common;

namespace PersonManagement.Application.Persons.Queries.SearchPersons
{
    public class SearchPersonsQueryValidator : AbstractValidator<SearchPersonsQuery>
    {
        public SearchPersonsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("Page size must be between 1 and 100.");

            RuleFor(x => x.FirstName)
            .Length(2, 50)
            .WithMessage("First name must be between 2 and 50 characters.")
            .Must(PersonValidationExtensions.BeOnlyLatinOrGeorgian)
            .WithMessage("First name must contain only Latin or Georgian letters.")
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

            RuleFor(x => x.LastName)
                .Length(2, 50)
                .WithMessage("Last name must be between 2 and 50 characters.")
                .Must(PersonValidationExtensions.BeOnlyLatinOrGeorgian)
                .WithMessage("Last name must contain only Latin or Georgian letters.")
                .When(x => !string.IsNullOrWhiteSpace(x.LastName));

            RuleFor(x => x.PersonalNumber)
                .Matches(@"^\d{11}$")
                .WithMessage("Personal number must be exactly 11 digits.")
                .When(x => !string.IsNullOrWhiteSpace(x.PersonalNumber));
        }
    }
}
