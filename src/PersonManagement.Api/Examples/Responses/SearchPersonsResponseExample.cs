using Swashbuckle.AspNetCore.Filters;
using PersonManagement.Application.Persons.Queries.SearchPersons;
using PersonManagement.Domain.Enums;

namespace PersonManagement.Api.Examples.Responses
{
    public class SearchPersonsResponseExample : IExamplesProvider<SearchPersonsResponse>
    {
        public SearchPersonsResponse GetExamples()
        {
            return new SearchPersonsResponse
            {
                TotalCount = 1,
                PageNumber = 1,
                PageSize = 10,
                Items =
                [
                    new PersonResponseModel
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        DateOfBirth = new DateTime(2003, 1, 1),
                        Gender = Gender.Male,
                        PersonalNumber = "12345678910"
                    }
                ]
            };
        }
    }
}
