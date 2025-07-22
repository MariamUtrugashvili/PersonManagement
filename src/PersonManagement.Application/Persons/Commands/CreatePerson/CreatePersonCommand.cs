using MediatR;
using PersonManagement.Application.Persons.Common;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;

namespace PersonManagement.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommand : PersonCommandBase, IRequest<CreatePersonResponse>
    {
    }
}