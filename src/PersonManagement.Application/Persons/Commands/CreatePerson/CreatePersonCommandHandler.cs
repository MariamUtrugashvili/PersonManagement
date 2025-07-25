using MediatR;
using PersonManagement.Application.Exceptions;
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
            var exists = await _personRepository.ExistsByPersonalNumberAsync(request.PersonalNumber, cancellationToken);
            if (exists)
                throw new PersonAlreadyExistsException(request.PersonalNumber);

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
            
            await _personRepository.AddAsync(person, cancellationToken);
            await _personRepository.SaveChangesAsync(cancellationToken);

            return new CreatePersonResponse { Id = person.Id };
        }
    }
}