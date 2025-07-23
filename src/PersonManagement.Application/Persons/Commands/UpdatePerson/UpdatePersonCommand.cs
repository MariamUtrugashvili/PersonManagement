using MediatR;
using PersonManagement.Application.Persons.Common;

namespace PersonManagement.Application.Persons.Commands.UpdatePerson
{
    /// <summary>
    /// Request to update an existing person.
    /// </summary>
    public class UpdatePersonCommand : PersonCommandBase, IRequest<Unit>
    {
        public int Id { get; set; }
    }
}