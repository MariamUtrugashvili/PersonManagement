using FluentValidation;

namespace PersonManagement.Application.Persons.Queries.GetPersonById
{
    public class GetPersonByIdQueryValidator : AbstractValidator<GetPersonByIdQuery>
    {
        public GetPersonByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Invalid person ID.");
        }
    }
}
