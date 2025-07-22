using FluentValidation;

namespace PersonManagement.Application.Persons.Queries.SearchPersons
{
    public class SearchPersonsQueryValidator : AbstractValidator<SearchPersonsQuery>
    {
        public SearchPersonsQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
