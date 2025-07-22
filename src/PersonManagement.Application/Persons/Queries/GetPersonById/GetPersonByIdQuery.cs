using MediatR;

namespace PersonManagement.Application.Persons.Queries.GetPersonById
{
    public class GetPersonByIdQuery : IRequest<GetPersonByIdResponse>
    {
        public int Id { get; set; }
    }
}