using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonManagement.Application.Persons.Queries.GetPersonById
{
    public class GetPersonByIdQuery : IRequest<GetPersonByIdResponse>
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}