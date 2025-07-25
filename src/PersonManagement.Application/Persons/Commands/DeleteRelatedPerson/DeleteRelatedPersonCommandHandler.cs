using MediatR;
using PersonManagement.Application.Caching;
using PersonManagement.Application.Constants;
using PersonManagement.Application.Exceptions;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.Persons.Commands.DeleteRelatedPerson
{
    public class DeleteRelatedPersonCommandHandler : IRequestHandler<DeleteRelatedPersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICacheService _cacheService;
        public DeleteRelatedPersonCommandHandler(IPersonRepository personRepository, ICacheService cacheService)
        {
            _personRepository = personRepository;
            _cacheService = cacheService;
        }

        public async Task<Unit> Handle(DeleteRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(
                id: request.PersonId,
                cancellationToken: cancellationToken,
                includeRelatedPersons: true,
                includePhoneNumbers: false) ?? throw new PersonNotFoundException(request.PersonId);

            var relatedPersonExists = await _personRepository.ExistsByIdAsync(request.RelatedToPersonId, cancellationToken);
            if (!relatedPersonExists)
                throw new PersonNotFoundException(request.RelatedToPersonId);

            var isRelated = person.RelatedPersons.Any(rp => rp.RelatedToPersonId == request.RelatedToPersonId);
            if (!isRelated)
                throw new RelationDoesNotExistException();

            person.RemoveRelatedPerson(request.RelatedToPersonId);

            await _personRepository.SaveChangesAsync(cancellationToken);

            await _cacheService.RemoveAsync(CacheConstants.GetPersonCacheKey(request.PersonId));

            return Unit.Value;
        }
    }
}