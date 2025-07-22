using MediatR;

namespace PersonManagement.Application.Persons.Commands.DeleteRelatedPerson
{
    public class DeleteRelatedPersonCommand : IRequest<Unit>
    {
        public int PersonId { get; init; }
        public int RelatedToPersonId { get; init; }
    }
}