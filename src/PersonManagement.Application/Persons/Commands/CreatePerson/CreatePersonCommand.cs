using MediatR;
using PersonManagement.Application.Persons.Common;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    /// <summary>
    /// Request to create a new person.
    /// </summary>
    public class CreatePersonCommand : PersonCommandBase, IRequest<CreatePersonResponse>
    {
    }
}