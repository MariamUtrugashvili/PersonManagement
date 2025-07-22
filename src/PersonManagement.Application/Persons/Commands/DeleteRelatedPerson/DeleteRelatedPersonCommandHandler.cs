using MediatR;
using PersonManagement.Application.Exceptions;
using PersonManagement.Domain.Enums;
using PersonManagement.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace PersonManagement.Application.Persons.Commands.DeleteRelatedPerson
{
    public class DeleteRelatedPersonCommandHandler : IRequestHandler<DeleteRelatedPersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        public DeleteRelatedPersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
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

            return Unit.Value;
        }
    }
}