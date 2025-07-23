using Swashbuckle.AspNetCore.Filters;
using PersonManagement.Application.Persons.Commands.AddRelatedPerson;
using PersonManagement.Domain.Enums;

namespace PersonManagement.Api.Examples.Requests
{
    public class AddRelatedPersonCommandExample : IExamplesProvider<AddRelatedPersonCommand>
    {
        public AddRelatedPersonCommand GetExamples()
        {
            return new AddRelatedPersonCommand
            {
                PersonId = 1,
                RelatedToPersonId = 2,
                RelationType = RelationType.Colleague
            };
        }
    }
}
