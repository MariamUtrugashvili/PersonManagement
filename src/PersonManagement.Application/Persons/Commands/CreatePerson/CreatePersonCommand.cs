using MediatR;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommand : IRequest<CreatePersonResponse>
    {
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public DateTime DateOfBirth { get; init; }
        public string PersonalNumber { get; init; } = null!;
        public Gender Gender { get; init; }
        public List<PhoneNumber> PhoneNumbers { get; init; } = [];
        public List<RelatedPerson> RelatedPersons { get; init; } = [];
    }
}