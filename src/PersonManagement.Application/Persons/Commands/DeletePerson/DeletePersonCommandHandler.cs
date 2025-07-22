using MediatR;
using PersonManagement.Application.Exceptions;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.Persons.Commands.DeletePerson
{
    public class DeletePersonCommandHandler(IPersonRepository personRepository) 
        : IRequestHandler<DeletePersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository = personRepository;

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

            return Unit.Value;
        }
    }
}