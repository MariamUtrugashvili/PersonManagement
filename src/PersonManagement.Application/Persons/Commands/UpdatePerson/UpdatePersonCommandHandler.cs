using MediatR;
using PersonManagement.Application.Caching;
using PersonManagement.Application.Constants;
using PersonManagement.Application.Exceptions;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICacheService _cacheService;
        public UpdatePersonCommandHandler(IPersonRepository personRepository, ICacheService cacheService)
        {
            _personRepository = personRepository;
            _cacheService = cacheService;
        }

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(
               id: request.Id,
               cancellationToken: cancellationToken,
               includeRelatedPersons: false,
               includePhoneNumbers: true) ?? throw new PersonNotFoundException(request.Id);

            person.UpdatePersonalInfo(
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request.Gender,
                request.PersonalNumber
            );
           
            var currentNumbers = person.PhoneNumbers.Select(p => p.Number).ToList();
            var newNumbers = request.PhoneNumbers.Select(p => p.Number).ToList();

            foreach (var number in currentNumbers.Except(newNumbers))
            {
                person.RemovePhoneNumber(number);
            }

            foreach (var phone in request.PhoneNumbers.Where(p => !currentNumbers.Contains(p.Number)))
            {
                person.AddPhoneNumber(phone.PhoneNumberType, phone.Number);
            }

            await _personRepository.SaveChangesAsync(cancellationToken);

            await _cacheService.RemoveAsync(CacheConstants.GetPersonCacheKey(request.Id));

            return Unit.Value;
        }
    }
}