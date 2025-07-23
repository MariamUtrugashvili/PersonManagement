using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonManagement.Application.Persons.Commands.DeletePerson
{
    public class DeletePersonCommand : IRequest<Unit>
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}