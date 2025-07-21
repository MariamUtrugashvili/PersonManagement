using MediatR;

namespace PersonManagement.Application.Persons.Commands.DeletePerson
{
    public class DeletePersonCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}