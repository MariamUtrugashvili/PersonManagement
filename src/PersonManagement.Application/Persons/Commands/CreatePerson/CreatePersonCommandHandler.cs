using MediatR;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CreatePersonResponse>
    {
        private readonly IPersonRepository _personRepository;
        public CreatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<CreatePersonResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = Person.Create(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.PersonalNumber,
            request.Gender
            );

            // Add phone numbers
            foreach (var phoneDto in request.PhoneNumbers)
            {
                person.AddPhoneNumber(phoneDto.PhoneNumberType, phoneDto.Number);
            }

            // Add related persons
            foreach (var relatedDto in request.RelatedPersons)
            {
                person.AddRelatedPerson(relatedDto.RelatedToPersonId, relatedDto.RelationType);
            }

            
            await _personRepository.AddAsync(person, cancellationToken);
            await _personRepository.SaveChangesAsync(cancellationToken);

            return new CreatePersonResponse(person.Id);
        }
    }
}