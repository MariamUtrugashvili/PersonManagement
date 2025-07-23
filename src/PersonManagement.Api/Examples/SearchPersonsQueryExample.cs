using Swashbuckle.AspNetCore.Filters;
using PersonManagement.Application.Persons.Queries.SearchPersons;

namespace PersonManagement.Api.Examples
{
    public class SearchPersonsQueryExample : IExamplesProvider<SearchPersonsQuery>
    {
        public SearchPersonsQuery GetExamples()
        {
            return new SearchPersonsQuery
            {
                FirstName = "John",
                LastName = "Doe",
                PersonalNumber = "1234567890",
                PageNumber = 1,
                PageSize = 10
            };
        }
    }
}
