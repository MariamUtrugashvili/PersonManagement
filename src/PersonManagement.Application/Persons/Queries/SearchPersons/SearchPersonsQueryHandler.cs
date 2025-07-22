using MediatR;
using PersonManagement.Domain.Repositories;

namespace PersonManagement.Application.Persons.Queries.SearchPersons
{
    public class SearchPersonsQueryHandler : IRequestHandler<SearchPersonsQuery, SearchPersonsResponse>
    {
        private readonly IPersonRepository _personRepository;
        public SearchPersonsQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<SearchPersonsResponse> Handle(SearchPersonsQuery request, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.SearchAsync(
                request.FirstName, request.LastName, request.PersonalNumber, request.PageNumber, request.PageSize, cancellationToken);

            return new SearchPersonsResponse
            {
                TotalCount = persons.Count,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Items = persons.Select(p => new PersonResponseModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    PersonalNumber = p.PersonalNumber
                }).ToList()
            };
        }
    }
}