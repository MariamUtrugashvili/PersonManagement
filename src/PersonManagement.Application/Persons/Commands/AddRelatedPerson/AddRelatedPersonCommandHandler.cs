using MediatR;
using PersonManagement.Application.Exceptions;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace PersonManagement.Application.Persons.Commands.AddRelatedPerson
{
    public class AddRelatedPersonCommandHandler : IRequestHandler<AddRelatedPersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        public AddRelatedPersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(AddRelatedPersonCommand request, CancellationToken cancellationToken)
        {
            if (request.PersonId == request.RelatedToPersonId)
                throw new RealtionIsInvalidException();

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

            return Unit.Value;
        }
    }
}