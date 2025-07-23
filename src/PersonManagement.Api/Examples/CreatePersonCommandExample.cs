using Swashbuckle.AspNetCore.Filters;
using PersonManagement.Application.Persons.Commands.CreatePerson;
using PersonManagement.Domain.Enums;
using PersonManagement.Application.Persons.Common;

namespace PersonManagement.Api.Examples
{
    public class CreatePersonCommandExample : IExamplesProvider<CreatePersonCommand>
    {
        public CreatePersonCommand GetExamples()
        {
            return new CreatePersonCommand
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2003, 1, 1),
                PersonalNumber = "01005030303",
                Gender = Gender.Male,
                PhoneNumbers =
                [
                    new PhoneNumberRequest { PhoneNumberType = PhoneNumberType.Mobile, Number = "555123123" }
                ]
            };
        }
    }
}
