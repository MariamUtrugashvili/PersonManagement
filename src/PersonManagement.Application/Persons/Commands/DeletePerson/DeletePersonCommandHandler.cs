using MediatR;
using PersonManagement.Application.Exceptions;
using PersonManagement.Domain.Repositories;
using PersonManagement.Application.Caching;
using PersonManagement.Application.Constants;

namespace PersonManagement.Application.Persons.Commands.DeletePerson
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICacheService _cacheService;
        public DeletePersonCommandHandler(IPersonRepository personRepository, ICacheService cacheService)
        {
            _personRepository = personRepository;
            _cacheService = cacheService;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(
                id: request.Id,
                cancellationToken: cancellationToken,
                includeRelatedPersons: false,
                includePhoneNumbers: true) ?? throw new PersonNotFoundException(request.Id);

            person.MarkAsDeleted();

            foreach (var number in person.PhoneNumbers)
            {
                person.RemovePhoneNumber(number.Number);
            }

            await _personRepository.SaveChangesAsync(cancellationToken);
   
            await _cacheService.RemoveAsync(CacheConstants.GetPersonCacheKey(request.Id));

            return Unit.Value;
        }
    }
}