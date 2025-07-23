using Swashbuckle.AspNetCore.Filters;
using PersonManagement.Application.Persons.Commands.DeleteRelatedPerson;

namespace PersonManagement.Api.Examples.Requests
{
    public class DeleteRelatedPersonCommandExample : IExamplesProvider<DeleteRelatedPersonCommand>
    {
        public DeleteRelatedPersonCommand GetExamples()
        {
            return new DeleteRelatedPersonCommand
            {
                PersonId = 1,
                RelatedToPersonId = 2
            };
        }
    }
}
