using MediatR;

namespace PersonManagement.Application.Persons.Queries.SearchPersons
{
    /// <summary>
    /// Request to search for persons.
    /// </summary>
    public class SearchPersonsQuery : IRequest<SearchPersonsResponse>
    {
        public string? Q { get; set; } // Free-text search
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalNumber { get; set; }
        
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}