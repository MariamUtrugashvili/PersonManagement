using MediatR;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;

namespace PersonManagement.Application.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; init; } = [];
    }
}