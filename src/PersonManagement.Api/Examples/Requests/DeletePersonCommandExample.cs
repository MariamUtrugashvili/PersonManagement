using Swashbuckle.AspNetCore.Filters;
using PersonManagement.Application.Persons.Commands.DeletePerson;

namespace PersonManagement.Api.Examples.Requests
{
    public class DeletePersonCommandExample : IExamplesProvider<DeletePersonCommand>
    {
        public DeletePersonCommand GetExamples()
        {
            return new DeletePersonCommand
            {
                Id = 1
            };
        }
    }
}
