using MediatR;
using PersonManagement.Application.Persons.Common;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    /// <summary>
    /// Request to create a new person.
    /// </summary>
    public class CreatePersonCommand : PersonCommandBase, IRequest<CreatePersonResponse>
    {
    }
}