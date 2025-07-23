using MediatR;
using PersonManagement.Application.Exceptions;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Repositories;
using PersonManagement.Application.Caching;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CreatePersonResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICacheService _cacheService;
        public CreatePersonCommandHandler(IPersonRepository personRepository, ICacheService cacheService)
        {
            _personRepository = personRepository;
            _cacheService = cacheService;
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

            var cacheKey = $"person:{person.Id}";
            await _cacheService.RemoveAsync(cacheKey);

            return new CreatePersonResponse { Id = person.Id };
        }
    }
}