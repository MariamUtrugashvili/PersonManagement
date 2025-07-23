using MediatR;
using PersonManagement.Application.Exceptions;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Repositories;
using PersonManagement.Application.Caching;
using System.Threading;
using System.Threading.Tasks;

namespace PersonManagement.Application.Persons.Commands.AddRelatedPerson
{
    public class AddRelatedPersonCommandHandler : IRequestHandler<AddRelatedPersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICacheService _cacheService;
        public AddRelatedPersonCommandHandler(IPersonRepository personRepository, ICacheService cacheService)
        {
            _personRepository = personRepository;
            _cacheService = cacheService;
        }

        public async Task<Unit> Handle(AddRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(
               id: request.PersonId,
               cancellationToken: cancellationToken,
               includeRelatedPersons: true,
               includePhoneNumbers: false) ?? throw new PersonNotFoundException(request.PersonId);

            var relatedPersonExists = await _personRepository.ExistsByIdAsync(request.RelatedToPersonId, cancellationToken);
            if (!relatedPersonExists)
                throw new PersonNotFoundException(request.RelatedToPersonId);

            var alreadyRelated = person.RelatedPersons.Any(rp => rp.RelatedToPersonId == request.RelatedToPersonId);
            if (alreadyRelated)
                throw new RelationAlreadyExistsException();

            person.AddRelatedPerson(request.RelatedToPersonId, request.RelationType);

            await _personRepository.SaveChangesAsync(cancellationToken);

            await _cacheService.RemoveAsync($"person:{request.PersonId}");

            return Unit.Value;
        }
    }
}