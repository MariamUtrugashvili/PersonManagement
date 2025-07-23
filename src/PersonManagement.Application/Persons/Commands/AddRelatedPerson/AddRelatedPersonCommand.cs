using MediatR;
using PersonManagement.Domain.Enums;

namespace PersonManagement.Application.Persons.Commands.AddRelatedPerson
{
    public class AddRelatedPersonCommand : IRequest<Unit>
    {
        public int PersonId { get; init; }
        public int RelatedToPersonId { get; init; }
        public RelationType RelationType { get; init; }
    }
}