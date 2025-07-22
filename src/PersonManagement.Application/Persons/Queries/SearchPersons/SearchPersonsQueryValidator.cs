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
                .Must(PersonValidationExtensions.BeOnlyLatinOrGeorgian)
                .WithMessage("First name must contain only Latin or Georgian letters.");

            RuleFor(x => x.LastName)
                .Length(2, 50)
                .Must(PersonValidationExtensions.BeOnlyLatinOrGeorgian)
                .WithMessage("Last name must contain only Latin or Georgian letters.");

            RuleFor(x => x.PersonalNumber)
                .Matches(@"^\d{11}$");
        }
    }
}
