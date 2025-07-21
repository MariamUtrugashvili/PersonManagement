using MediatR;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.Persons.Commands.DeletePerson
{
    public class DeletePersonCommandHandler(IPersonRepository personRepository) 
        : IRequestHandler<DeletePersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            await _personRepository.DeleteAsync(request.Id, cancellationToken);
            await _personRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}