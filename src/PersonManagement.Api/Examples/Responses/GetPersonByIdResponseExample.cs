using Swashbuckle.AspNetCore.Filters;
using PersonManagement.Application.Persons.Queries.GetPersonById;
using PersonManagement.Domain.Enums;

namespace PersonManagement.Api.Examples.Responses
{
    public class GetPersonByIdResponseExample : IExamplesProvider<GetPersonByIdResponse>
    {
        public GetPersonByIdResponse GetExamples()
        {
            return new GetPersonByIdResponse
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2003, 1, 1),
                PersonalNumber = "01005030303",
                PhoneNumbers =
                [
                    new PhoneNumberResponse { Id = 1, Number = "555123123", PhoneNumberType = PhoneNumberType.Mobile }
                ],
                RelatedPersons =
                [
                    new RelatedPersonResponse
                    {
                        Id = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        DateOfBirth = new DateTime(1992, 2, 2),
                        Gender = Gender.Female,
                        PersonalNumber = "9876543210",
                        PhoneNumbers =
                        [
                        new PhoneNumberResponse { Id = 2, Number = "555654321", PhoneNumberType = PhoneNumberType.Mobile }
                        ]
                    }
                ]
            };
        }
    }
}
